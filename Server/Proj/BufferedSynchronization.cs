using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Packet;
using Server.Component;
using Server.ObjectEntity;

namespace Server {
    class BufferedSynchronization {

        public FieldMap Owner;
        public Dictionary<int, Dictionary<string, string>> SyncDataToSend = new();
        public Dictionary<int, Dictionary<string, string>> SyncDataToBroadcast = new();

        public void OnPropertyChanged(int handle, string property, string value) {
            var broadcastLookup = Lookup.BroadcastSyncPropertyList;
            var sendLookup = Lookup.SendSyncPropertyList;

            if (broadcastLookup.Contains(property)) {
                if (SyncDataToBroadcast.ContainsKey(handle) == false) {
                    SyncDataToBroadcast[handle] = new Dictionary<string, string>();
                }

                SyncDataToBroadcast[handle][property] = value;
            } else if (sendLookup.Contains(property)) {
                if (SyncDataToSend.ContainsKey(handle) == false) {
                    SyncDataToSend[handle] = new Dictionary<string, string>();
                }

                SyncDataToSend[handle][property] = value;
            }
        }

        public void InitBufferedSync(FieldMap fieldMap) {
            Owner = fieldMap;
        }

        public void SendSyncData() {
            if (SyncDataToSend.Count <= 0) {
                return;
            }

            var entities = Owner.ClientManager.GetClientEntities();
            foreach (var syncData in SyncDataToSend) {
                foreach (var entity in entities) {
                    if (entity.Handle == syncData.Key) {
                        var pks = new PKS_ZC_SYNC_PROPERTY_LIST {
                            Handle = syncData.Key,
                            SyncData = syncData.Value
                        };

                        var clientComp = entity.GetComponent<ClientComponent>();
                        clientComp.SendPacket(pks);
                        break;
                    }
                }
            }

            SyncDataToSend.Clear();
        }

        public void BroadcastSyncData() {
            if (SyncDataToBroadcast.Count <= 0) {
                return;
            }
            var entities = Owner.ClientManager.GetClientEntities();
            foreach (var syncData in SyncDataToBroadcast) {
                var pks = new PKS_ZC_SYNC_PROPERTY_LIST {
                    Handle = syncData.Key,
                    SyncData = syncData.Value
                };

                foreach (var entity in entities) {
                    var clientComp = entity.GetComponent<ClientComponent>();
                    clientComp.SendPacket(pks);
                }
            }

            SyncDataToBroadcast.Clear();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using Server.Enum;
using Server.Message;
using Server.Manager;

namespace Server {
    class FieldMap {
        public string ClassName {
            get;
            set;
        }
        public int ClassId;

        public NPCGenerator NpcGenerator = new();

        public List<FieldObject> FieldObjects = new();
        public List<Client> Clients = new();

        public List<int> RemoveRequestedFieldObjectHandle = new();

        public void OnInitialize() {
            // 초기 NPC 생성 (풀젠)
            NpcGenerator.InitGenerator(this);

        }

        public void OnUpdate(double dt) {
            UpdateFieldObjects(dt);
            ExecuteRemoval();
        }

        public void UpdateFieldObjects(double dt) {
            foreach (var fieldObject in FieldObjects) {
                fieldObject.OnUpdate(dt);
            }
        }

        public void RemoveFieldObject(int handle) {
            RemoveRequestedFieldObjectHandle.Add(handle);
        }

        private void ExecuteRemoval() {
            if (RemoveRequestedFieldObjectHandle.Count <= 0) {
                return;
            }
            foreach (var handle in RemoveRequestedFieldObjectHandle) {
                for (int i = 0; i < FieldObjects.Count; ++i) {
                    if (FieldObjects[i].Handle == handle) {
                        FieldObjects[i] = null;
                        FieldObjects.Remove(FieldObjects[i]);
                        break;
                    }
                }
            }
        }

        public void AddPlayerCharacter(Client client, DBPlayerInfo info) {
            var jobName = info.JobName;
            var level = info.Level;
            var position = info.Position;

            var player = FieldObjectFactory.Inst.CreateFieldChar(Idspace.PC, this, client, jobName, level, position);
            FieldObjects.Add(player);
            Clients.Add(client);
        }


        public FieldObject GetFieldObject(int handle) {
            foreach (var fieldObject in FieldObjects) {
                if (fieldObject.Handle == handle) {
                    return fieldObject;
                }
            }

            return null;
        }

        public FieldChar GetFieldChar(int handle) {
            foreach (var fieldObject in FieldObjects) {
                if (fieldObject is FieldChar fieldChar) {
                    if (fieldChar.Handle == handle) {
                        return fieldChar;
                    }
                }
            }

            return null;
        }

        public List<FieldChar> GetPlayerList() {
            var playerList = new List<FieldChar>();
            foreach (var fieldObject in FieldObjects) {
                if (fieldObject.Idspace != Idspace.PC) {
                    continue;
                }

                playerList.Add((FieldChar)fieldObject);
            }

            return playerList;
        }

        // TODO: 1.need to improve performance / 2.find listed fo by handle
        public List<FieldObject> GetCollidedFieldObjects(FieldObject self) {
            var collidedFieldObjects = new List<FieldObject>();

            foreach (var fieldObject in FieldObjects) {
                if (self.IsCollided(fieldObject)) {
                    collidedFieldObjects.Add(fieldObject);
                }
            }

            return collidedFieldObjects;
        }

        public List<FieldChar> GetCollidedFieldChars(FieldObject self) {
            var collidedFieldChar = new List<FieldChar>();

            foreach (var fieldObject in FieldObjects) {
                if (self.IsCollided(fieldObject)) {
                    if (fieldObject is FieldChar fieldChar) {
                        collidedFieldChar.Add(fieldChar);
                    }
                }
            }

            return collidedFieldChar;
        }
    }
}

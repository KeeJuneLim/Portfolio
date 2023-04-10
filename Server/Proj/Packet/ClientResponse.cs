using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Packet;
using Server.Component;

namespace Server {
    class ClientResponse {
        public static void OnResponseClientMessage(Client client, PKS_CZ_BROADCAST_ENTERED_MAP pks) {
            var clients = client.Component.CurrentMap.ClientManager.GetClientEntities();
            var packet = new PKS_ZC_NOTIFY_CLIENT_ENTERED_MAP {
                IsSuccess = true,
                Port = client.remoteAddress.Port
            };

            foreach (var clientEntity in clients) {
                var clientComp = clientEntity.GetComponent<ClientComponent>();
                clientComp.SendPacket(packet);
            }
        }


        public static void OnResponseClientMessage(Client client, PKS_CZ_REQUEST_ECHO pks) {
            if (pks.IsSuccess) {
                var echo = new PKS_ZC_RESPONSE_ECHO{
                    IsSuccess = true,
                    Command = $"Echo - {pks.Command}"
                };

                Console.WriteLine($"[{client.remoteAddress.Port} << {echo.Command}]");
                
                client.Send(echo);
            }
        }
    }
}

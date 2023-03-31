using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Packet;

namespace Server {
    class ClientResponse {
        public static void OnResponseClientMessage(Client client, PKS_CZ_BROADCAST_ENTERED_MAP pks) {
            var players = client.Owner.CurrentMap.GetPlayerList();
            foreach (var player in players) {
                var clientInfo = new PKS_ZC_NOTIFY_CLIENT_ENTERED_MAP {
                    IsSuccess = true,
                    Port = client.remoteAddress.Port
                };

                player.Client.Send(clientInfo);
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

using System;
using Packet;

namespace Server.ClientReceive {
    class Packet {
        public static void OnReceiveClientMessage(Client client, PKS_CZ_REQUEST_ECHO pks) {
            if (pks.IsSuccess) {
                var message = pks.Command;

                Console.WriteLine($"[{client.remoteAddress.Port}] >> {message}");
                if (message.Equals("exit", StringComparison.OrdinalIgnoreCase)) {
                    client.Disconnect();
                    return;
                }

                ClientResponse.ClientResponse.OnResponseClientMessage(client, pks);
            }
        }

        public static void OnReceiveClientMessage(Client client, PKS_CZ_TEST2 pks) {

        }

    }
}

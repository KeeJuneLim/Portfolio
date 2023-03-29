using System;
using Packet;

namespace Bot {
    class Response {
        public static void OnReceiveClientMessage(Client client, PKS_ZC_RESPONSE_ECHO pks) {
            if (pks.IsSuccess) {
                if (pks.IsSuccess) {
                    var message = pks.Command;
                    Console.WriteLine($"[Server] >> {message}");
                }
            }
        }

        public static void OnReceiveClientMessage(Client client, PKS_ZC_TEST pks) {

        }

    }
}

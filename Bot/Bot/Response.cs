using System;
using System.Net;
using Packet;

namespace Bot {
    class Response {
        public static void OnReceiveClientMessage(Client client, PKS_ZC_RESPONSE_ECHO pks) {
            if (pks.IsSuccess) {
                var message = pks.Command;
                var localAddress = (IPEndPoint)client?.ConnectSocket?.LocalEndPoint;

                if (localAddress != null) {
                    Console.WriteLine($"[{localAddress.Port}] << {message}");
                }
            }
        }

        public static void OnReceiveClientMessage(Client client, PKS_ZC_TEST pks) {

        }

    }
}

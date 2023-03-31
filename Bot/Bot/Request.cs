using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Packet;

namespace Bot {
    class Request {

        public static void BroadcastEnteredMap(Client client) {
            var pks = new PKS_CZ_BROADCAST_ENTERED_MAP {
                IsSuccess = true,
            };

            client.Send(pks);
        }

        public static void RequestEcho(Client client, string command) {
            var pks = new PKS_CZ_REQUEST_ECHO {
                IsSuccess = true,
                Command = command
            };

            client.Send(pks);
        }
    }
}

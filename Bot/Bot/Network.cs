using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Bot {
    class Network {
        private const string IpAddress = "121.166.194.83";
        private const string IpAddress_LocalHost = "127.0.0.1";
        private const int Port = 7000;

        private Socket socket;

        public Network() {


        }

        public void Connect() {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.NoDelay = true;
            var client = new Client(new IPEndPoint(IPAddress.Parse(IpAddress_LocalHost), Port));

            var pending = socket.ConnectAsync(client);

            // when requested as async but responded sync, pending will be 'false'
            //if (pending == false) {
            //    client.OnClientConnected(null, client);
            //}
            Console.WriteLine($"Local Endpoint: {socket.LocalEndPoint}");


            while (true) {
                // 콘솔로 부터 메시지를 받으면 서버로 보낸다.
                string command = Console.ReadLine();

                var pks = new PKS_CZ_TEST {
                    IsSuccess = true,
                    Command = command
                };

                client.Send(pks);
                // exit면 종료한다.
                if ("exit".Equals(pks.Command, StringComparison.OrdinalIgnoreCase)) {
                    break;
                }
            }
        }
    }
}

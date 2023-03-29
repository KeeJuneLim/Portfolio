using System;
using System.Net;
using System.Net.Sockets;
using Packet;

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

            var client = new Client(new IPEndPoint(IPAddress.Parse(IpAddress_LocalHost), Port));
            var pending = socket.ConnectAsync(client);

            // in case which AcceptAsync won't work as async : Completed never invokes
            // when requested as async but responded sync, pending will be 'false'
            if (pending == false) {
                client.OnClientConnected(null, client);
            }

            while (true) {
                // 콘솔로 부터 메시지를 받으면 서버로 보낸다.
                string command = Console.ReadLine();

                Request.RequestEcho(client, command);


                // exit면 종료한다.
                if ("exit".Equals(command, StringComparison.OrdinalIgnoreCase)) {
                    break;
                }
            }
        }
    }
}

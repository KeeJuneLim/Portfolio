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
        private Client client;


        public void Connect() {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            client = new Client(new IPEndPoint(IPAddress.Parse(IpAddress_LocalHost), Port));

            var pending = socket.ConnectAsync(client);

            // in case which AcceptAsync won't work as async : Completed never invokes
            // when requested as async but responded sync, pending will be 'false'
            if (pending == false) {
                client.OnClientConnected(null, client);
            }

            RequestEchoWithRandomNumber();
        }

        private double requestInterval = 0.5;
        private long lastRequestedTime = 0;
        public void RequestEchoWithRandomNumber() {
            double requestInterval = 0.5;
            var generator = new Random();
            while (true) {
                var curUpdateTime = DateTime.Now.Ticks;
                var dt = (curUpdateTime - lastRequestedTime) / 10000000;
                if (dt <= requestInterval) {
                    continue;
                }

                var number = generator.Next(0, 1000);
                var localAddress = (IPEndPoint)socket.LocalEndPoint;
                if (localAddress != null) {
                    Console.WriteLine($"[{localAddress.Port}] >> {number}");
                }

                Request.RequestEcho(client, number.ToString());
                lastRequestedTime = DateTime.Now.Ticks;
            }
        }

        public void RequestEchoWithInputString() {
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

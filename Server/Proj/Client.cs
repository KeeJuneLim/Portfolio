using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Proj {
    class Client : SocketAsyncEventArgs {
        private Socket socket;

        private IPEndPoint remoteAddress;

        public Client(Socket s) {
            socket = s;
            SetBuffer(new byte[1024], 0, 1024);
            UserToken = socket;

            Completed += OnReceiveClientMessage;
            socket.ReceiveAsync(this);

            remoteAddress = (IPEndPoint)socket.RemoteEndPoint;
            Console.WriteLine($"Client : (From: {remoteAddress.Address}:{remoteAddress.Port}, Connection time: {DateTime.Now})");

            // send result to client
            var pks = new PKS_CZ_TEST {
                IsSuccess = true,
                Command = "Connected to Server!"
            };
            Send(pks);
        }

        // invoke when received message from client
        private void OnReceiveClientMessage(object sender, SocketAsyncEventArgs e) {
            // connected
            if (socket.Connected && BytesTransferred > 0) {
                var data = e.Buffer;

                SetBuffer(new byte[1024], 0, 1024);

                var pks = ZeroFormatter.ZeroFormatterSerializer.Deserialize<PKS_CZ_TEST>(data);

                if (pks.IsSuccess) {
                    var message = pks.Command;

                    Console.WriteLine(message);
                    if (message.Equals("exit", StringComparison.OrdinalIgnoreCase)) {
                        Disconnect();
                        return;
                    }

                    // for test
                    var echo = new PKS_CZ_TEST {
                        IsSuccess = true,
                        Command = $"Echo - {pks.Command}"
                    };

                    Send(echo);
                }

                socket.ReceiveAsync(this);
            } else {
                //disconnected
                Disconnect();
            }
        }


        // could be made as async, but not necessarily
        //private SocketAsyncEventArgs sendArgs = new();
        private void Send(PKS_BASE pks) {
            if (pks is PKS_CZ_TEST packet) {
                var sendData = ZeroFormatter.ZeroFormatterSerializer.Serialize<PKS_CZ_TEST>(packet);
                Console.WriteLine($"Sending Message To {remoteAddress.Address}:{remoteAddress.Port}: [{packet.Command}]");
                socket.Send(sendData, sendData.Length, SocketFlags.None);
            }

            //if (pks is PKS_CZ_TEST packet) {
            //    var sendData = ZeroFormatter.ZeroFormatterSerializer.Serialize<PKS_CZ_TEST>(packet);
            //    sendArgs.SetBuffer(sendData, 0, sendData.Length);
            //    socket.SendAsync(sendArgs);
            //}
        }

        private void Disconnect() {
            SetBuffer(null, 0, 0);
            Completed -= OnReceiveClientMessage;
            socket.DisconnectAsync(this);
            Dispose();

            Console.WriteLine($"Disconnected :  (From: {remoteAddress.Address}:{remoteAddress.Port}, Connection time: {DateTime.Now})");
        }
    }





}

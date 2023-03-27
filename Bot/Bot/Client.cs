using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;

namespace Bot {
    class Client : SocketAsyncEventArgs {
        private Socket socket;

        public Client(EndPoint remoteEndPoint) {
            RemoteEndPoint = remoteEndPoint;
            Completed += OnClientConnected;
        }

        public void OnClientConnected(object sender, SocketAsyncEventArgs e) {
            // disconnect connected event
            Completed -= OnClientConnected;

            // init socket infos
            socket = e.ConnectSocket;
            UserToken = this.socket;

            SetBuffer(new byte[1024], 0, 1024);

            // connect receive message event
            Completed += OnReceiveServerMessage;
            Console.WriteLine("ReceiveEvent");

            // invoke IOCP
            socket.ReceiveAsync(this);
        }

        // invoke when received message from server
        private void OnReceiveServerMessage(object sender, SocketAsyncEventArgs e) {
            // connected
            if (socket.Connected && BytesTransferred > 0) {
                byte[] data = e.Buffer;

                SetBuffer(new byte[1024], 0, 1024);
                var pks = ZeroFormatter.ZeroFormatterSerializer.Deserialize<PKS_CZ_TEST>(data);

                if (pks.IsSuccess) {
                    var message = pks.Command;
                    Console.WriteLine(message);
                }

                // invoke IOCP
                socket.ReceiveAsync(this);
            } else {
                // disconnected
                Disconnect();
            }
        }

        // could be made as async, but not necessarily
        // private SocketAsyncEventArgs sendArgs = new SocketAsyncEventArgs();
        public void Send(PKS_BASE pks) {
            if (pks is PKS_CZ_TEST packet) {
                var sendData = ZeroFormatter.ZeroFormatterSerializer.Serialize(packet);
                socket.Send(sendData, sendData.Length, SocketFlags.None);
            }

            //sendArgs.SetBuffer(sendData, 0, sendData.Length);
            //socket.SendAsync(sendArgs);
        }

        private void Disconnect() {
            //SetBuffer(null, 0, 0);
            //Completed -= OnReceiveServerMessage;
            //socket.DisconnectAsync(this);
            //Dispose();

            var remoteAddress = (IPEndPoint)socket.RemoteEndPoint;
            Console.WriteLine($"Disconnected :  (From: {remoteAddress.Address}:{remoteAddress.Port}, Connection time: {DateTime.Now})");
        }
    }
}

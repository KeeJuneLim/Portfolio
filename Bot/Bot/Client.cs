using System;
using System.Net;
using System.Net.Sockets;
using Bot;
using Packet;
using ZeroFormatter;

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
            Completed += OnReceiveClientMessage;

            // invoke IOCP
            socket.ReceiveAsync(this);
        }

        // invoke when received message from server
        private void OnReceiveClientMessage(object sender, SocketAsyncEventArgs e) {
            // connected
            if (socket.Connected && BytesTransferred > 0) {
                byte[] data = e.Buffer;

                SetBuffer(new byte[1024], 0, 1024);
                var headerLength = data[0];
                var typeData = new byte[headerLength];

                for (int i = 0; i < headerLength; ++i) {
                    typeData[i] = data[i + 1];
                }

                var typeName = ZeroFormatterSerializer.Deserialize<string>(typeData);

                switch (typeName) {
                    case "Packet.PKS_ZC_RESPONSE_ECHO":
                        var pks_zc_response_echo = ZeroFormatterSerializer.Deserialize<PKS_ZC_RESPONSE_ECHO>(data, headerLength + 1);
                        Response.OnReceiveClientMessage(this, pks_zc_response_echo);
                        break;

                    case "Packet.PKS_ZC_NOTIFY_CLIENT_ENTERED_MAP":
                        var notify_enter = ZeroFormatterSerializer.Deserialize<PKS_ZC_NOTIFY_CLIENT_ENTERED_MAP>(data, headerLength + 1);
                        Response.OnReceiveClientMessage(this, notify_enter);
                        break;

                    case "Packet.PKS_ZC_TEST":
                        var pks_zc_test = ZeroFormatterSerializer.Deserialize<PKS_ZC_TEST>(data, headerLength + 1);
                        Response.OnReceiveClientMessage(this, pks_zc_test);
                        break;
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
            var typeName = pks.GetType().FullName;
            var typeArr = ZeroFormatterSerializer.Serialize(typeName);
            byte typeSize = (byte)typeArr.Length;

            byte[] data = new byte[1024];
            switch (typeName) {
                case "Packet.PKS_CZ_REQUEST_ECHO":
                    data = ZeroFormatterSerializer.Serialize(pks as PKS_CZ_REQUEST_ECHO);
                    break;
                case "Packet.PKS_CZ_BROADCAST_ENTERED_MAP":
                    data = ZeroFormatterSerializer.Serialize(pks as PKS_CZ_BROADCAST_ENTERED_MAP);
                    break;
                case "Packet.PKS_CZ_TEST2":
                    data = ZeroFormatterSerializer.Serialize(pks as PKS_CZ_TEST2);
                    break;
            }

            var sendData = new byte[data.Length + typeArr.Length + 1];

            sendData[0] = typeSize;
            typeArr.CopyTo(sendData, 1);
            data.CopyTo(sendData, typeArr.Length + 1);

            socket.Send(sendData, sendData.Length, SocketFlags.None);
        }

        private void Disconnect() {
            SetBuffer(null, 0, 0);
            Completed -= OnReceiveClientMessage;
            socket.DisconnectAsync(this);
            Dispose();

            var remoteAddress = (IPEndPoint)socket.RemoteEndPoint;
            if (remoteAddress != null) {
                Console.WriteLine($"[{remoteAddress.Port}] - Disconnected, Connection time: {DateTime.Now})");
            }
        }
    }
}

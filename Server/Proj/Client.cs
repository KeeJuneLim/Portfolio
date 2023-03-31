using System;
using System.Net;
using System.Net.Sockets;
using Packet;
using Server.Manager;
using ZeroFormatter;

namespace Server {
    class Client : SocketAsyncEventArgs {
        private Socket socket;
        internal IPEndPoint remoteAddress;
        public FieldChar Owner;

        public Client(Socket s) {
            socket = s;
            socket.Blocking = false;
            SetBuffer(new byte[1024], 0, 1024);
            UserToken = socket;

            Completed += OnReceiveClientMessage;
            socket.ReceiveAsync(this);

            remoteAddress = (IPEndPoint)socket.RemoteEndPoint;
            Console.WriteLine($"[{remoteAddress.Port}] - Connected, Connection time: {DateTime.Now})");
        }

        // invoke when received message from client
        private void OnReceiveClientMessage(object sender, SocketAsyncEventArgs e) {
            // connected
            if (socket.Connected && BytesTransferred > 0) {
                var data = e.Buffer;

                SetBuffer(new byte[1024], 0, 1024);

                var headerLength = data[0];
                var typeData = new byte[headerLength];

                for (int i = 0; i < headerLength; ++i) {
                    typeData[i] = data[i + 1];
                }

                var typeName = ZeroFormatterSerializer.Deserialize<string>(typeData);
                
                switch (typeName) {
                    case "Packet.PKS_CZ_REQUEST_ECHO":
                        var pks_cz_test = ZeroFormatterSerializer.Deserialize<PKS_CZ_REQUEST_ECHO>(data, headerLength + 1);
                        ClientReceive.OnReceiveClientMessage(this, pks_cz_test);
                        break;
                    case "Packet.PKS_CZ_BROADCAST_ENTERED_MAP":
                        var pks_cz_broadcast_entered_map = ZeroFormatterSerializer.Deserialize<PKS_CZ_BROADCAST_ENTERED_MAP>(data, headerLength + 1);
                        ClientReceive.OnReceiveClientMessage(this, pks_cz_broadcast_entered_map);
                        break;

                    case "Packet.PKS_CZ_TEST2":
                        var pks_cz_test2 = ZeroFormatterSerializer.Deserialize<PKS_CZ_TEST2>(data, headerLength + 1);
                        ClientReceive.OnReceiveClientMessage(this, pks_cz_test2);
                        break;
                }

                socket.ReceiveAsync(this);
            } else {
                //disconnected
                Disconnect();
            }
        }


        // could be made as async, but not necessarily
        private SocketAsyncEventArgs sendArgs = new();
        internal void Send(PKS_BASE pks) {
            var typeName = pks.GetType().FullName;
            var typeArr = ZeroFormatterSerializer.Serialize(typeName);
            byte typeSize = (byte)typeArr.Length;
            byte[] data = new byte[1024];
            switch (typeName) {
                case "Packet.PKS_ZC_RESPONSE_ECHO":
                    data = ZeroFormatterSerializer.Serialize(pks as PKS_ZC_RESPONSE_ECHO);
                    break;
                case "Packet.PKS_ZC_NOTIFY_CLIENT_ENTERED_MAP":
                    data = ZeroFormatterSerializer.Serialize(pks as PKS_ZC_NOTIFY_CLIENT_ENTERED_MAP);
                    break;

                case "Packet.PKS_ZC_TEST":
                    data = ZeroFormatterSerializer.Serialize(pks as PKS_ZC_TEST);
                    break;
            }

            var sendData = new byte[data.Length + typeArr.Length + 1];

            sendData[0] = typeSize;
            typeArr.CopyTo(sendData, 1);
            data.CopyTo(sendData, typeArr.Length + 1);


            sendArgs.SetBuffer(sendData, 0, sendData.Length);
            socket.SendAsync(sendArgs);
        }

        internal void Disconnect() {
            // TODO: remove client from fieldmap
            //ClientManager.Inst.RemoveClient(this);
            SetBuffer(null, 0, 0);
            Completed -= OnReceiveClientMessage;
            socket.DisconnectAsync(this);
            Dispose();

            Console.WriteLine($"Disconnected :  (From: {remoteAddress.Address}:{remoteAddress.Port}, Connection time: {DateTime.Now})");
        }
    }
}

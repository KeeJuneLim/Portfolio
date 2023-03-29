using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Server.Manager;

namespace Server {
    class Server {
        private Socket Socket;
        private SocketAsyncEventArgs Args = new();

        private const string LoopAddress = "127.0.0.1";
        private const int Port = 7000;

        public void Run() {
            Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Socket.Bind(new IPEndPoint(IPAddress.Any, Port));
            Socket.Listen(20);

            Args.UserToken = Socket;
            Args.Completed += OnClientConnected;

            // in case which AcceptAsync won't work as async : Completed never invokes
            var pending = Socket.AcceptAsync(Args);
            if (pending == false) {
                OnClientConnected(null, Args);
            }
        }

        // invoke when client connects
        private void OnClientConnected(object sender, SocketAsyncEventArgs e) {
            var client = new Client(e.AcceptSocket);
            ClientManager.Inst.AddClient(client);
            e.AcceptSocket = null;
            Socket.AcceptAsync(e);
        }
    }
}

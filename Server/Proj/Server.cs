using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Proj {


    class Server : SocketAsyncEventArgs {
        private Socket socket;
        private List<Client> Clients = new();

        private int Index;

        public Server(Socket s) {
            socket = s;
            UserToken = socket;


            Completed += OnClientConnected;
            RegisterAccept(this);


        }

        // invoke when client connects
        private void OnClientConnected(object sender, SocketAsyncEventArgs e) {
            var client = new Client(e.AcceptSocket);
            e.AcceptSocket = null;

            socket.AcceptAsync(e);
        }

        private void RegisterAccept(SocketAsyncEventArgs e) {
          
        }
    }
}

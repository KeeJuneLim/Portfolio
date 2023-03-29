using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Manager {
    class ClientManager {
        private static ClientManager instance;
        private static List<Client> Clients = new();

        public static ClientManager Inst => instance ??= new ClientManager();

        public void AddClient(Client client) {
            Clients.Add(client);
        }

        public void RemoveClient(Client client) {
            Console.WriteLine($"Current Client Count: {Clients.Count}");
        }
    }
}

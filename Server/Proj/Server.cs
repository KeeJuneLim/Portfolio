using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
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

            //should request db to get player's info, then register in fieldmap
            // TODO: DBConnections
            var playerInfo = new DBPlayerInfo {
                Level = 1,
                MaxHP = 100,
                HP = 90,
                AttackPower = 10,
                Defense = 5,
                CharacterName = "HelloWorld",
                JobName = "Paladin",
                LocatedFieldMapClassId = 3001,
                Position = new Vector2(5, 5)
            };

            FieldMapManager.Inst.RegisterPlayer(client, playerInfo);

            e.AcceptSocket = null;
            Socket.AcceptAsync(e);
        }
    }

    //TODO: when db process is done, this should be move to correct directory
    public class DBPlayerInfo {
        public int Level;
        public int MaxHP;
        public int HP;
        public int AttackPower;
        public int Defense;
        public string CharacterName;
        public string JobName;
        public int LocatedFieldMapClassId;
        public Vector2 Position;

    }
}

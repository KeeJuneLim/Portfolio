using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using System.Text;
using Proj.Enum;
using ZeroFormatter;

namespace Proj {
    class Program {
        private const int FPS = 1;
        private static double lastUpdateTime;

        private const string IpAddress = "121.166.194.83";
        private const string LocalHostIp = "127.0.0.1";
        private const int Port = 7000;

        //private double 
        static void Main(string[] args) {
            OnInitialize();

            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //socket.Bind(new IPEndPoint(IPAddress.Parse(LocalHostIp), Port));
            socket.Bind(new IPEndPoint(IPAddress.Any, Port));
            socket.Listen(20);
            socket.AcceptAsync(new Server(socket));
        
            Console.WriteLine("Server Connected");

            while (true) {
                var curUpdateTime = DateTime.Now.Ticks;
                var dt = (curUpdateTime - lastUpdateTime) / 10000000;
                var updateInterval = (double)1 / FPS;

                if (dt >= updateInterval) {
                    lastUpdateTime = DateTime.Now.Ticks;

                    OnUpdate(dt);
                }
            }
        }

        static void OnInitialize() {
            lastUpdateTime = DateTime.Now.Ticks;
            Parser.LoadXmlData();

            FieldMapManager.Inst.OnInitialize();
        }


        static void OnUpdate(double dt) {
            FieldMapManager.Inst.OnUpdate(dt);
        }

    }
}

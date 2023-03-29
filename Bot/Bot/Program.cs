using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Bot {
    class Program {
        private const string IpAddress = "121.166.194.83";
        private const string IpAddress_LocalHost = "127.0.0.1";
        private const int Port = 7000;

        private static List<Thread> threads = new();

        private const int BotCount = 1;
        static void Main(string[] args) {

            for (int i = 0; i < BotCount; ++i) {
                var network = new Network();
                threads.Add(new Thread(network.Connect));
            }

            foreach (var thread in threads) {
                thread.Start();
            }

        }
    }
}

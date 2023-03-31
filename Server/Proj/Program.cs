using System;
using Server.Manager;

namespace Server {
    class Program {
        private const int FPS = 1;
        private static double lastUpdateTime;

        static void Main(string[] args) {
            OnInitialize();

            var server = new Server();
            server.Run();

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

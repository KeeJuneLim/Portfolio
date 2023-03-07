using System;

namespace Proj {
    class Program {
        private const int FPS = 30;
        private static double lastUpdateTime;
        //private double 
        static void Main(string[] args) {
            OnInitialize();

            var barbarianValue = Parser.GetInt("Barbarian", "Value");
            Console.WriteLine(barbarianValue); // 출력값: 6

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
        }


        static void OnUpdate(double dt) {
        }

    }
}

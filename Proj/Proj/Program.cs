using System;

namespace Proj {
    class Program {
        private const int FPS = 1;
        private static double lastUpdateTime;
        //private double 
        static void Main(string[] args) {
            OnInitialize();

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

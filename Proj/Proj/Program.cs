using System;

namespace Proj {
    class Program
    {
        private const int UpdateInterval = 33; // ms

        //private double 
        static void Main(string[] args) {
            OnInitialize();


            Parser.TestFunc();


            Console.WriteLine($"args:{args}");
        }

        static void OnInitialize()
        {


        }


        static void OnUpdate()
        {

        }

        static void OnEnd()
        {

        }
    }
}

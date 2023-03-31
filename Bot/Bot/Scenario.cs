using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Bot {
    class ScenarioName {
        public const string RequestEchoWithRandomNumber = "RequestEchoWithRandomNumber";
        public const string RequestEchoWithInputString = "RequestEchoWithInputString";
        public const string BroadcastWhenConnected = "BroadcastWhenConnected";


    }
    //class ScenarioTest {
    //    private double requestInterval = 0.5;
    //    private long lastRequestedTime = 0;
    //    public void RequestEchoWithRandomNumber(Client client) {
    //        double requestInterval = 0.5;
    //        var generator = new Random();
    //        while (true) {
    //            var curUpdateTime = DateTime.Now.Ticks;
    //            var dt = (curUpdateTime - lastRequestedTime) / 10000000;
    //            if (dt <= requestInterval) {
    //                continue;
    //            }

    //            var number = generator.Next(0, 1000);
    //            var localAddress = (IPEndPoint)socket.LocalEndPoint;
    //            if (localAddress != null) {
    //                Console.WriteLine($"[{localAddress.Port}] >> {number}");
    //            }

    //            Request.RequestEcho(client, number.ToString());
    //            lastRequestedTime = DateTime.Now.Ticks;
    //        }
    //    }

    //    public void RequestEchoWithInputString(Client client) {
    //        while (true) {
    //            // 콘솔로 부터 메시지를 받으면 서버로 보낸다.
    //            string command = Console.ReadLine();

    //            Request.RequestEcho(client, command);


    //            // exit면 종료한다.
    //            if ("exit".Equals(command, StringComparison.OrdinalIgnoreCase)) {
    //                break;
    //            }
    //        }
    //    }

    //}




}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Enum;

namespace Server {
    class Lookup {
        public static readonly List<string> BroadcastSyncPropertyList = new() {
            PropName.MaxHP,
            PropName.HP,
            PropName.Level,
            PropName.Position,

        };

        public static readonly List<string> SendSyncPropertyList = new() {
            PropName.Exp,
            PropName.AttackPower,
            PropName.Defense
        };
    }
}

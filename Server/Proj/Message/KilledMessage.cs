﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Message {
    class KilledMessage : BaseMessage {
        public int KilledObjectHandle;
        public int RewardExp;
        public int RewardMoney;
    }
}

using System;
using System.Runtime.InteropServices;
using ZeroFormatter;

namespace Proj {

    [ZeroFormattable]
    [Serializable]
    public class PKS_BASE {
        private Type Type;

        public PKS_BASE() {
            Type = GetType();
        }
    }


    [ZeroFormattable]
    [Serializable]
    public class PKS_CZ_TEST : PKS_BASE{
        [Index(0)] public virtual bool IsSuccess { get; set; }
        [Index(1)] public virtual string Command { get; set; }
    }

    [ZeroFormattable]
    [Serializable]
    public class PKS_CZ_TEST2 : PKS_BASE {
        [Index(0)] public virtual bool IsSuccess { get; set; }
        [Index(1)] public virtual string Command { get; set; }
        [Index(2)] public virtual string Command2 { get; set; }
    }




    [ZeroFormattable]
    [Serializable]
    public class TestPacket {
        [Index(0)] 
        public virtual bool IsSuccess { get; set; }

        [Index(1)] 
        public virtual int IntValue { get; set; }



    }

}

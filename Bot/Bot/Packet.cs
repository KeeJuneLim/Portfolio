using System;
using System.Runtime.InteropServices;
using ZeroFormatter;

namespace Bot {


    [ZeroFormattable]
    [Serializable]
    public class PKS_BASE {
    }


    [ZeroFormattable]
    [Serializable]
    public class PKS_CZ_TEST : PKS_BASE {
        [Index(0)] public virtual bool IsSuccess { get; set; }
        [Index(1)] public virtual string Command { get; set; }
    }

}

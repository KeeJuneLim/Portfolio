using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using ZeroFormatter;

namespace Packet {

    [ZeroFormattable]
    [Serializable]
    public class PKS_BASE {
    }


    [ZeroFormattable]
    [Serializable]
    public class PKS_CZ_BROADCAST_ENTERED_MAP : PKS_BASE {
        [Index(0)] public virtual bool IsSuccess { get; set; }
    }

    [ZeroFormattable]
    [Serializable]
    public class PKS_CZ_REQUEST_ECHO : PKS_BASE{
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


    ////////////////////////////////


    [ZeroFormattable]
    [Serializable]
    public class PKS_ZC_NOTIFY_CLIENT_ENTERED_MAP : PKS_BASE {
        [Index(0)] public virtual bool IsSuccess { get; set; }
        [Index(1)] public virtual int Port{ get; set; }
    }

    [ZeroFormattable]
    [Serializable]
    public class PKS_ZC_RESPONSE_ECHO: PKS_BASE {
        [Index(0)] public virtual bool IsSuccess { get; set; }
        [Index(1)] public virtual string Command { get; set; }
    }

    [ZeroFormattable]
    [Serializable]
    public class PKS_ZC_SYNC_PROPERTY_LIST : PKS_BASE {
        [Index(0)] public virtual int Handle { get; set; }

        [Index(1)] public virtual Dictionary<string, string> SyncData { get; set; }
    }


    [ZeroFormattable]
    [Serializable]
    public class PKS_ZC_TEST : PKS_BASE {
        [Index(0)] public virtual bool IsSuccess { get; set; }
        [Index(1)] public virtual string Command { get; set; }
    }
}

using System;

namespace KcpSharp
{
    [Flags]
    internal enum KcpProbeType
    {
        None = 0,
        AskSend = 1,
        AskTell = 2,
    }
}

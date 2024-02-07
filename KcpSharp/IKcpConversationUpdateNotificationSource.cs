using System;

namespace KcpSharp
{
    internal interface IKcpConversationUpdateNotificationSource
    {
        ReadOnlyMemory<byte> Packet { get; }
        void Release();
    }
}

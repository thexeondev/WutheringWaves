using Protocol;

namespace GameServer.Controllers.Attributes;

[AttributeUsage(AttributeTargets.Method)]
internal class NetEventAttribute : Attribute
{
    public MessageId MessageId { get; }

    public NetEventAttribute(MessageId netMessageId)
    {
        MessageId = netMessageId;
    }
}

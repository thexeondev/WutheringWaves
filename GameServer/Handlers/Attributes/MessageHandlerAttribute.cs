using Protocol;

namespace GameServer.Handlers.Attributes;

[AttributeUsage(AttributeTargets.Method)]
internal class MessageHandlerAttribute : Attribute
{
    public MessageId MessageId { get; }

    public MessageHandlerAttribute(MessageId messageId)
    {
        MessageId = messageId;
    }
}

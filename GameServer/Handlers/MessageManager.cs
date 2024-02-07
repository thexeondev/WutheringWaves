using GameServer.Handlers.Factory;
using GameServer.Network;
using GameServer.Network.Messages;
using GameServer.Network.Packets;
using Protocol;

namespace GameServer.Handlers;

internal delegate Task MessageHandler(IServiceProvider serviceProvider, ReadOnlyMemory<byte> data);
internal class MessageManager
{
    private readonly MessageHandlerFactory _handlerFactory;
    private readonly IServiceProvider _serviceProvider;

    public MessageManager(IServiceProvider serviceProvider, MessageHandlerFactory handlerFactory)
    {
        _handlerFactory = handlerFactory;
        _serviceProvider = serviceProvider;
    }

    public async Task<bool> ProcessMessage(MessageId messageId, ReadOnlyMemory<byte> data)
    {
        MessageHandler? handler = _handlerFactory.GetHandler(messageId);
        if (handler != null)
        {
            await handler(_serviceProvider, data);
            return true;
        }

        return false;
    }

    public static void EncodeMessage(Memory<byte> buffer, BaseMessage message)
    {
        buffer.Span[0] = (byte)message.Type;
        message.Encode(buffer);
    }

    public static BaseMessage DecodeMessage(ReadOnlyMemory<byte> buffer)
    {
        MessageType type = (MessageType)buffer.Span[0];

        BaseMessage message = type switch
        {
            MessageType.Request => new RequestMessage(),
            MessageType.Response => new ResponseMessage(),
            MessageType.Push => new PushMessage(),

            _ => throw new NotSupportedException("Message type not implemented: " + type)
        };

        message.Decode(buffer);
        return message;
    }
}

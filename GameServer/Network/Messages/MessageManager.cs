using GameServer.Controllers.Factory;
using Protocol;

namespace GameServer.Network.Messages;

internal delegate Task PushHandler(IServiceProvider serviceProvider, ReadOnlySpan<byte> data);
internal delegate Task<RpcResult> RpcHandler(IServiceProvider serviceProvider, ReadOnlySpan<byte> data);
internal class MessageManager
{
    private readonly EventHandlerFactory _handlerFactory;
    private readonly IServiceProvider _serviceProvider;

    public MessageManager(IServiceProvider serviceProvider, EventHandlerFactory handlerFactory)
    {
        _handlerFactory = handlerFactory;
        _serviceProvider = serviceProvider;
    }

    public async Task<RpcResult?> ExecuteRpc(MessageId messageId, ReadOnlyMemory<byte> data)
    {
        RpcHandler? handler = _handlerFactory.GetRpcHandler(messageId);
        if (handler != null)
            return await handler(_serviceProvider, data.Span);

        return null;
    }

    public async Task<bool> HandlePush(MessageId messageId, ReadOnlyMemory<byte> data)
    {
        PushHandler? handler = _handlerFactory.GetPushHandler(messageId);
        if (handler == null) return false;

        await handler(_serviceProvider, data.Span);
        return true;
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

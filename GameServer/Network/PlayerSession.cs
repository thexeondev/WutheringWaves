using GameServer.Network.Messages;
using GameServer.Network.Rpc;
using Google.Protobuf;
using Microsoft.Extensions.Logging;
using Protocol;

namespace GameServer.Network;
internal class PlayerSession
{
    private readonly ILogger _logger;
    private readonly MessageManager _messageManager;

    public RpcManager Rpc { get; }
    public ISessionActionListener? Listener { private get; set; }

    public PlayerSession(ILogger<PlayerSession> logger, MessageManager messageManager, RpcManager rpcManager)
    {
        _logger = logger;
        _messageManager = messageManager;
        Rpc = rpcManager;
    }

    public async Task HandleMessageAsync(BaseMessage message)
    {
        switch (message)
        {
            case RequestMessage request:
                await Rpc.Execute(request);
                break;
            case PushMessage push:
                if (!await _messageManager.HandlePush(push.MessageId, push.Payload))
                    _logger.LogWarning("Push message ({id}) was not handled", push.MessageId);

                break;
        }
    }

    public Task Push<TProtoBuf>(MessageId id, TProtoBuf data) where TProtoBuf : IMessage<TProtoBuf>
    {
        return Listener?.OnServerMessageAvailable(new PushMessage
        {
            MessageId = id,
            Payload = data.ToByteArray()
        }) ?? Task.CompletedTask;
    }

    public Task SendRpcRsp(ResponseMessage message)
    {
        return Listener?.OnServerMessageAvailable(message) ?? Task.CompletedTask;
    }
}

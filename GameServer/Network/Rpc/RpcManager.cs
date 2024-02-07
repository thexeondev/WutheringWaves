using GameServer.Handlers;
using GameServer.Network.Messages;
using Google.Protobuf;
using Microsoft.Extensions.Logging;
using Protocol;

namespace GameServer.Network.Rpc;
internal class RpcManager
{
    private readonly IRpcEndPoint _endPoint;
    private readonly ILogger _logger;
    private readonly MessageManager _messageManager;
    private ushort _curId;

    public RpcManager(MessageManager messageManager, IRpcEndPoint endPoint, ILogger<RpcManager> logger)
    {
        _endPoint = endPoint;
        _logger = logger;
        _messageManager = messageManager;
    }

    public async Task HandleRpcRequest(RequestMessage request)
    {
        _logger.LogInformation("Processing Rpc, message: {msg_id}, id: {rpc_id}", request.MessageId, request.RpcID);

        _curId = request.RpcID;
        _ = await _messageManager.ProcessMessage(request.MessageId, request.Payload);

        if (_curId != 0)
        {
            _logger.LogWarning("Rpc was not handled properly (message: {msg_id}, id: {rpc_id})", request.MessageId, request.RpcID);
            _curId = 0;
        }
    }

    public async Task ReturnAsync<TProtoBuf>(MessageId messageId, TProtoBuf data) where TProtoBuf : IMessage<TProtoBuf>
    {
        if (_curId == 0) throw new InvalidOperationException("RpcManager::ReturnAsync called - no rpc being processed!");

        await _endPoint.SendRpcResult(new ResponseMessage
        {
            MessageId = messageId,
            RpcID = _curId,
            Payload = data.ToByteArray()
        });

        _logger.LogInformation("Rpc with id {rpc_id} was handled, return message: {msg_id}", _curId, messageId);
        _curId = 0;
    }
}

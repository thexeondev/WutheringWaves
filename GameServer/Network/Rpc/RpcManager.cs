using GameServer.Network.Messages;
using Microsoft.Extensions.Logging;

namespace GameServer.Network.Rpc;
internal class RpcManager
{
    private readonly IRpcEndPoint _endPoint;
    private readonly ILogger _logger;
    private readonly MessageManager _messageManager;

    public RpcManager(MessageManager messageManager, IRpcEndPoint endPoint, ILogger<RpcManager> logger)
    {
        _endPoint = endPoint;
        _logger = logger;
        _messageManager = messageManager;
    }

    public async Task Execute(RequestMessage request)
    {
        ResponseMessage? response = await _messageManager.ExecuteRpc(request.MessageId, request.Payload);
        if (response == null)
        {
            _logger.LogWarning("Rpc was not handled properly (message: {msg_id}, id: {rpc_id})", request.MessageId, request.RpcID);
            return;
        }

        response.RpcID = request.RpcID;
        await _endPoint.SendRpcResult(response);

        _logger.LogInformation("Rpc with id {rpc_id} was handled, return message: {msg_id}", request.RpcID, response.MessageId);
    }
}

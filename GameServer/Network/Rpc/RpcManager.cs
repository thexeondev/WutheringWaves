using GameServer.Network.Messages;
using GameServer.Systems.Event;
using Microsoft.Extensions.Logging;

namespace GameServer.Network.Rpc;
internal class RpcManager
{
    private readonly IRpcEndPoint _endPoint;
    private readonly ILogger _logger;
    private readonly MessageManager _messageManager;
    private readonly EventSystem _eventSystem;

    public RpcManager(MessageManager messageManager, IRpcEndPoint endPoint, ILogger<RpcManager> logger, EventSystem eventSystem)
    {
        _endPoint = endPoint;
        _logger = logger;
        _messageManager = messageManager;
        _eventSystem = eventSystem;
    }

    public async Task Execute(RequestMessage request)
    {
        RpcResult? result = await _messageManager.ExecuteRpc(request.MessageId, request.Payload);
        if (result == null)
        {
            _logger.LogWarning("Rpc was not handled properly (message: {msg_id}, id: {rpc_id})", request.MessageId, request.RpcID);
            return;
        }

        result.Response.RpcID = request.RpcID;
        await _endPoint.SendRpcResult(result.Response);

        foreach (GameEventType postEvent in result.PostEvents)
        {
            await _eventSystem.Emit(postEvent);
        }

        _logger.LogInformation("Rpc with id {rpc_id} was handled, return message: {msg_id}", request.RpcID, result.Response.MessageId);
    }
}

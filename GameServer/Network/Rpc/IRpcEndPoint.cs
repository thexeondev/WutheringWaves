using GameServer.Network.Messages;

namespace GameServer.Network.Rpc;
internal interface IRpcEndPoint
{
    Task SendRpcResult(ResponseMessage message);
}

using GameServer.Network.Messages;
using GameServer.Systems.Event;

namespace GameServer.Controllers.Result;
internal class RpcResult
{
    public ResponseMessage Response { get; }
    public List<GameEventType> PostEvents { get; }

    public RpcResult(ResponseMessage response)
    {
        Response = response;
        PostEvents = [];
    }

    public RpcResult AddPostEvent(GameEventType type)
    {
        PostEvents.Add(type);
        return this;
    }
}

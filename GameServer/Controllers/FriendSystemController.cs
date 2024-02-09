using GameServer.Controllers.Attributes;
using GameServer.Network;
using GameServer.Network.Messages;
using Protocol;

namespace GameServer.Controllers;
internal class FriendSystemController : Controller
{
    public FriendSystemController(PlayerSession session) : base(session)
    {
        // FriendMessageHandler.
    }

    [NetEvent(MessageId.FriendAllRequest)]
    public ResponseMessage OnFriendAllRequest() => Response(MessageId.FriendAllResponse, new FriendAllResponse());
}

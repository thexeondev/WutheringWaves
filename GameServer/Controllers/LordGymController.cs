using GameServer.Controllers.Attributes;
using GameServer.Network;
using GameServer.Network.Messages;
using Protocol;

namespace GameServer.Controllers;
internal class LordGymController : Controller
{
    public LordGymController(PlayerSession session) : base(session)
    {
        // LordGymMessageHandler.
    }

    [NetEvent(MessageId.LordGymInfoRequest)]
    public ResponseMessage OnLordGymInfoRequest() => Response(MessageId.LordGymInfoResponse, new LordGymInfoResponse());
}

using GameServer.Controllers.Attributes;
using GameServer.Network;
using GameServer.Network.Messages;
using Protocol;

namespace GameServer.Controllers;
internal class InfluenceReputationController : Controller
{
    public InfluenceReputationController(PlayerSession session) : base(session)
    {
        // InfluenceReputationMessageHandler.
    }

    [NetEvent(MessageId.InfluenceInfoRequest)]
    public ResponseMessage OnInfluenceInfoRequest() => Response(MessageId.InfluenceInfoResponse, new InfluenceInfoResponse());
}

using GameServer.Controllers.Attributes;
using GameServer.Network;
using GameServer.Network.Messages;
using Protocol;

namespace GameServer.Controllers;
internal class DailyActivityController : Controller
{
    public DailyActivityController(PlayerSession session) : base(session)
    {
        // DailyActivityController.
    }

    [NetEvent(MessageId.ActivityRequest)]
    public RpcResult OnActivityRequest() => Response(MessageId.ActivityResponse, new ActivityResponse());

    [NetEvent(MessageId.LivenessRequest)]
    public RpcResult OnLivenessRequest() => Response(MessageId.LivenessResponse, new LivenessResponse());
}

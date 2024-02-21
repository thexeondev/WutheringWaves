using GameServer.Controllers.Attributes;
using GameServer.Network;
using GameServer.Network.Messages;
using Protocol;

namespace GameServer.Controllers;
internal class AchievementController : Controller
{
    public AchievementController(PlayerSession session) : base(session)
    {
        // AchievementController.
    }

    [NetEvent(MessageId.AchievementInfoRequest)]
    public RpcResult OnAchievementInfoRequest() => Response(MessageId.AchievementInfoResponse, new AchievementInfoResponse());
}

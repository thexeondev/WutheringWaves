using GameServer.Controllers.Attributes;
using GameServer.Network;
using GameServer.Network.Messages;
using Protocol;

namespace GameServer.Controllers;
internal class TowerController : Controller
{
    public TowerController(PlayerSession session) : base(session)
    {
        // TowerController.
    }

    [NetEvent(MessageId.TowerChallengeRequest)]
    public RpcResult OnTowerChallengeRequest() => Response(MessageId.TowerChallengeResponse, new TowerChallengeResponse());

    [NetEvent(MessageId.CycleTowerChallengeRequest)]
    public RpcResult OnCycleTowerChallengeRequest() => Response(MessageId.CycleTowerChallengeResponse, new CycleTowerChallengeResponse());
}

using GameServer.Controllers.Attributes;
using GameServer.Network;
using Protocol;

namespace GameServer.Controllers;
internal class RoguelikeController : Controller
{
    public RoguelikeController(PlayerSession session) : base(session)
    {
        // RoguelikeController.
    }

    [NetEvent(MessageId.RoguelikeSeasonDataRequest)]
    public RpcResult OnRoguelikeSeasonDataRequest() => Response(MessageId.RoguelikeSeasonDataResponse, new RoguelikeSeasonDataResponse());
}

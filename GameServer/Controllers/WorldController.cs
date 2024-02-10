using GameServer.Controllers.Attributes;
using GameServer.Network;
using GameServer.Network.Messages;
using GameServer.Systems.Event;
using Protocol;

namespace GameServer.Controllers;
internal class WorldController : Controller
{
    public WorldController(PlayerSession session) : base(session)
    {
        // WorldController.
    }

    [GameEvent(GameEventType.EnterGame)]
    public async Task OnEnterGame(CreatureController creatureController)
    {
        await creatureController.JoinScene(8);
    }

    [NetEvent(MessageId.EntityOnLandedRequest)]
    public ResponseMessage OnEntityOnLandedRequest() => Response(MessageId.EntityOnLandedResponse, new EntityOnLandedResponse());

    [NetEvent(MessageId.PlayerMotionRequest)]
    public ResponseMessage OnPlayerMotionRequest() => Response(MessageId.PlayerMotionResponse, new PlayerMotionResponse());

    [NetEvent(MessageId.EntityLoadCompleteRequest)]
    public ResponseMessage OnEntityLoadCompleteRequest() => Response(MessageId.EntityLoadCompleteResponse, new EntityLoadCompleteResponse());

    [NetEvent(MessageId.UpdateSceneDateRequest)]
    public ResponseMessage OnUpdateSceneDateRequest() => Response(MessageId.UpdateSceneDateResponse, new UpdateSceneDateResponse());
}

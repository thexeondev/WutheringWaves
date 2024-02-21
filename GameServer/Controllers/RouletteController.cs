using GameServer.Controllers.Attributes;
using GameServer.Models;
using GameServer.Network;
using GameServer.Network.Messages;
using GameServer.Systems.Entity;
using GameServer.Systems.Entity.Component;
using GameServer.Systems.Event;
using Protocol;

namespace GameServer.Controllers;
internal class RouletteController : Controller
{
    public RouletteController(PlayerSession session) : base(session)
    {
        // RouletteController.
    }

    [GameEvent(GameEventType.EnterGame)]
    public async Task OnEnterGame()
    {
        await Session.Push(MessageId.ExploreToolAllNotify, new ExploreToolAllNotify
        {
            SkillList = { 3001, 3002, 1005, 1006, 1001, 1004, 1003, 1007, 1009 },
            ExploreSkill = 1001
        });

        await Session.Push(MessageId.ExploreSkillRouletteUpdateNotify, new ExploreSkillRouletteUpdateNotify
        {
            RouletteInfo =
            {
                new ExploreSkillRoulette
                {
                    SkillIds = {1001, 1004, 1003, 0, 0, 0, 0, 0},
                },
                new ExploreSkillRoulette
                {
                    SkillIds = {10002, 10004, 0, 0, 0, 0, 0, 0},
                }
            }
        });
    }

    [NetEvent(MessageId.VisionExploreSkillSetRequest)]
    public async Task<RpcResult> OnVisionExploreSkillSetRequest(VisionExploreSkillSetRequest request, CreatureController creatureController, EventSystem eventSystem)
    {
        PlayerEntity? playerEntity = creatureController.GetPlayerEntity();
        if (playerEntity == null) return Response(MessageId.VisionExploreSkillSetResponse, new VisionExploreSkillSetResponse { ErrCode = (int)ErrorCode.PlayerNotInAnyScene });

        EntityVisionSkillComponent visionSkillComponent = playerEntity.ComponentSystem.Get<EntityVisionSkillComponent>();
        visionSkillComponent.SetExploreTool(request.SkillId);

        await eventSystem.Emit(GameEventType.VisionSkillChanged);

        return Response(MessageId.VisionExploreSkillSetResponse, new VisionExploreSkillSetResponse
        {
            SkillId = request.SkillId
        });
    }
}

using GameServer.Controllers.Attributes;
using GameServer.Controllers.Event;
using GameServer.Network;
using GameServer.Network.Messages;
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
            SkillList = { 1001, 1004, 1003 },
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
                    SkillIds = {1001, 1004, 1003, 0, 0, 0, 0, 0},
                }
            }
        });
    }

    [NetEvent(MessageId.VisionExploreSkillSetRequest)]
    public async Task<ResponseMessage> OnVisionExploreSkillSetRequest(VisionExploreSkillSetRequest request)
    {
        await Session.Push(MessageId.VisionSkillChangeNotify, new VisionSkillChangeNotify
        {
            EntityId = 1,
            VisionSkillInfos =
            {
                new VisionSkillInformation
                {
                    SkillId = request.SkillId
                }
            }
        });

        return Response(MessageId.VisionExploreSkillSetResponse, new VisionExploreSkillSetResponse
        {
            SkillId = request.SkillId
        });
    }
}

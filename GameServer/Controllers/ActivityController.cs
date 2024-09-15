using Core.Config;
using GameServer.Controllers.Attributes;
using GameServer.Extensions.Logic;
using GameServer.Models;
using GameServer.Network;
using GameServer.Systems.Event;
using GameServer.Systems.Notify;
using Protocol;

namespace GameServer.Controllers;
internal class ActivityController : Controller
{
    private readonly ModelManager _modelManager;
    private readonly ConfigManager _configManager;
    public ActivityController(PlayerSession session, ConfigManager configManager, ModelManager modelManager) : base(session)
    {
        // ActivityController.
        _modelManager = modelManager;
        _configManager = configManager;
    }

    [NetEvent(MessageId.ActivityRequest)]
    public async Task<RpcResult> OnActivityRequest()
    {
        List<ActivityData> data = [];
        SignActivity sign = new() { SignStateList = { 1, 1, 1, 1, 1, 1, 1 }, KeepTime = Int32.MaxValue };
        ActivityData activityData = new() { BeginOpenTime = 0, BeginShowTime = 0, EndShowTime = Int32.MaxValue, Id = 100100001, IsFirstOpen = true, IsUnlock = true, Type = 2, SignActivity = sign };
        data.Add(activityData);

        activityData = new ActivityData { BeginOpenTime = 0, BeginShowTime = 0, EndShowTime = Int32.MaxValue, Id = 120000001, IsFirstOpen = true, IsUnlock = true, Type = 200, CompletePreQuests = { 310000043 } };
        data.Add(activityData);

        activityData = new ActivityData { BeginOpenTime = 0, BeginShowTime = 0, EndShowTime = Int32.MaxValue, Id = 100300001, IsFirstOpen = true, IsUnlock = true, Type = 3, CompletePreQuests = { 310000041 } };

        data.Add(activityData);
        activityData = new ActivityData { BeginOpenTime = 0, BeginShowTime = 0, EndShowTime = Int32.MaxValue, Id = 100, IsFirstOpen = true, IsUnlock = true, Type = 0 };
        data.Add(activityData);

        WorldNewJourneyActivity worldEnd = new() { WorldNewJourneyEndTime = Int32.MaxValue };
        activityData = new ActivityData { BeginOpenTime = 0, BeginShowTime = 0, EndShowTime = Int32.MaxValue, Id = 100500001, IsFirstOpen = true, IsUnlock = true, Type = 5, WorldNewJourneyActivity = worldEnd };
        data.Add(activityData);

        await Session.Push(MessageId.InstPlayDataNotify, new InstPlayDataNotify { Id = 995 });

        await Session.Push(MessageId.AddUnlockedGuideNotify, new AddUnlockedGuideNotify { UnlockedGuideIds = { 310000043, 310000041, 139000003 } });

        await Session.Push(MessageId.QuestReadyListNotify, new QuestReadyListNotify { QuestId = { 310000043, 310000041, 139000003, 10010001 } });
        await Session.Push(MessageId.QuestListNotify, new QuestListNotify { Quests = { new QuestInfo { QuestId = 10010001, Status = 1 }, new QuestInfo { QuestId = 139000003, Status = 1 }, new QuestInfo { QuestId = 310000043, Status = 1 }, new QuestInfo { QuestId = 310000041, Status = 1 } } });
        await Session.Push(MessageId.QuestShowListNotify, new QuestShowListNotify { QuestId = { 310000043, 310000041, 139000003, 10010001 } });

        return Response(MessageId.ActivityResponse, new ActivityResponse { Activities = { data } });

    }

    [NetEvent(MessageId.TowerGuideActivityInfoRequest)]
    public RpcResult OnTowerGuideActivityInfoRequest() => Response(MessageId.TowerGuideActivityInfoResponse, new TowerGuideActivityInfoResponse { TowerGuideId = { 1, 2 } });

    [NetEvent(MessageId.AdviceRequest)]
    public RpcResult OnAdviceRequest() => Response(MessageId.AdviceResponse, new AdviceResponse { Advices = { new PbAdvice { AreaId = 704, Id = -1, Contents = { new PbAdviceContent { Id = 1, Type = 1, Word = 1 } }, UpVote = 1 } } });


    [NetEvent(MessageId.ParkourChallengeRequest)]
    public RpcResult OnParkourChallengeRequest() => Response(MessageId.ParkourChallengeResponse, new ParkourChallengeResponse { OpenIds = { 1 }, Challenges = { new ParkourChallenge { ChallengeId = 1, MinDuration = 1, TakenScores = { 0 }, MaxScore = 0 } } });

    [GameEvent(GameEventType.EnterGame)]
    public async Task LivenessRefreshNotify()
    {
        _modelManager.Liveness.ClearLivenessTask();
        foreach (LivenesstaskConfig livenesstaskConfig in _configManager.Enumerate<LivenesstaskConfig>())
        {
            _modelManager.Liveness.AddLivenessTask(livenesstaskConfig.TaskId, 1, 1, true, false, true);
        }
        LivenessRefreshNotify notify = new();
        {
            notify.LivenessInfo = new()
            {
                LivenessCount = 200,
                DayEnd = 86401,
                RewardedLiveness = { 50002, 50003, 50004, 50005, 50006 },
                Tasks = { _modelManager.Liveness.LivenessTaskList }
            };
        }
        await Session.Push(MessageId.LivenessRefreshNotify, notify);
    }
    [NetEvent(MessageId.LivenessTakeRequest)]
    public RpcResult OnLivenessTakeRequest(/*ActivityRequest request*/)
    {
        return Response(MessageId.LivenessTakeResponse, new LivenessTakeResponse
        {
            ErrorCode = 0,
            Ids = { },
            ItemMap = { },
        });
    }

}

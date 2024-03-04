using Core.Config;
using GameServer.Controllers.Attributes;
using GameServer.Extensions.Logic;
using GameServer.Models;
using GameServer.Network;
using GameServer.Systems.Event;
using GameServer.Systems.Notify;
using Protocol;

namespace GameServer.Controllers;
internal class QuestController : Controller
{
    private readonly ModelManager _modelManager;
    private readonly ConfigManager _configManager;


    //private QuestData  QuestData { get;set} = new();
    public QuestController(PlayerSession session, ConfigManager configManager, ModelManager modelManager) : base(session)
    {
        _modelManager = modelManager;
        _configManager = configManager;


    }

    [GameEvent(GameEventType.EnterGame)]
    public async Task OnQuestListNotify()
    {
        _modelManager.Quest.ClearQuest();
        foreach (QuestConfig questconfig in _configManager.Enumerate<QuestConfig>())
        {
            _modelManager.Quest.AddQuest(questconfig.Id,(int)QuestState.Ready);
        }

        QuestListNotify notify = new();
        {
            notify.Quests.Add(_modelManager.Quest.QuestInfoList);
        }
        await Session.Push(MessageId.QuestListNotify, notify);
    }

    [GameEvent(GameEventType.EnterGame)]
    public async Task OnQuestReadyListNotify()
    {
        _modelManager.Quest.QuestIdList.Clear();
        foreach (QuestConfig questconfig in _configManager.Enumerate<QuestConfig>())
        {
            _modelManager.Quest.AddQuestids(questconfig.Id);
        }

        QuestReadyListNotify notify = new();
        {
            notify.QuestId.Add(_modelManager.Quest.QuestIdList);
        }
        await Session.Push(MessageId.QuestReadyListNotify, notify);
    }


    //[NetEvent(MessageId.LivenessRequest)]
    //public RpcResult OnLivenessRequest() => Response(MessageId.LivenessResponse, new LivenessResponse());

    [NetEvent(MessageId.QuestRedDotRequest)]
    public RpcResult OnQuestRedDotRequest(/*QuestRedDotRequest request*/) //=> Response(MessageId.QuestRedDotResponse, new QuestRedDotResponse());
    {
        //questId_ = other.questId_;
        //operate_ = other.operate_;
        QuestRedDotResponse response = new();
        {
            //errorId_ = other.errorId_;
            response.ErrorId = (int)ErrorCode.Success;
        }
        return Response(MessageId.QuestRedDotResponse, response);   
    }

    [NetEvent(MessageId.TraceQuestRequest)]
    public RpcResult OnTraceQuestRequest(TraceQuestRequest request)
    { 
        //traceType_ = other.traceType_;
        //questId_ = other.questId_;
        //operate_ = other.operate_;

        //TraceQuestNotify ()
        if (request.QuestId != 0)
        {
            TraceQuestResponse response = new();
            {
                response.ErrorId = (int)ErrorCode.Success;
            }
            return Response(MessageId.TraceQuestResponse, response);
        }
        else
        {
            TraceQuestResponse response = new();
            {
                response.ErrorId = (int)ErrorCode.ErrQuestTraceType;
            }
            return Response(MessageId.TraceQuestResponse, response);
        }
    }







}


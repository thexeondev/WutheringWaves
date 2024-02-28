using GameServer.Controllers.Attributes;
using GameServer.Network;
using Protocol;

namespace GameServer.Controllers;
internal class AchievementController : Controller
{
    public AchievementController(PlayerSession session) : base(session)
    {
        // AchievementController.
    }

    [NetEvent(MessageId.AchievementInfoRequest)]
    public RpcResult OnAchievementInfoRequest(/*AchievementInfoRequest request*/)
    {


        return Response(MessageId.AchievementInfoResponse, new AchievementInfoResponse
        {
            AchievementGroupInfoList =
             {
                 new AchievementGroupInfo
                  {
                   AchievementGroupEntry = new AchievementGroupEntry//GroupID
                    {
                    Id = 3001,
                    FinishTime = 0,
                    IsReceive = true
                    }
                  },
                  new AchievementGroupInfo
                  {
                   AchievementGroupEntry = new AchievementGroupEntry
                    {
                    Id = 3002,
                    FinishTime = 0,
                    IsReceive = true
                    }
                  },
                   new AchievementGroupInfo
                  {
                   AchievementGroupEntry = new AchievementGroupEntry
                    {
                    Id = 3003,
                    FinishTime = 0,
                    IsReceive = true
                    }
                  },
                   new AchievementGroupInfo
                   {
                    AchievementGroupEntry = new AchievementGroupEntry
                    {
                    Id = 3004,
                    FinishTime = 0,
                    IsReceive = true
                    }
                   },
                   new AchievementGroupInfo
                  {
                   AchievementGroupEntry = new AchievementGroupEntry
                    {
                    Id = 1001,
                    FinishTime = 0,
                    IsReceive = true
                    }
                  },
                   new AchievementGroupInfo
                  {
                   AchievementGroupEntry = new AchievementGroupEntry
                    {
                    Id = 1004,
                    FinishTime = 0,
                    IsReceive = true
                    }
                  },
                   new AchievementGroupInfo
                  {
                   AchievementGroupEntry = new AchievementGroupEntry
                    {
                    Id = 4001,
                    FinishTime = 0,
                    IsReceive = true
                    }
                  },
                   new AchievementGroupInfo
                  {
                   AchievementGroupEntry = new AchievementGroupEntry
                    {
                    Id = 4002,
                    FinishTime = 0,
                    IsReceive = true
                    }
                  },
                   new AchievementGroupInfo
                  {
                   AchievementGroupEntry = new AchievementGroupEntry
                    {
                    Id = 5001,
                    FinishTime = 0,
                    IsReceive = true
                    }
                  },
                   new AchievementGroupInfo
                  {
                   AchievementGroupEntry = new AchievementGroupEntry
                    {
                    Id = 3005,
                    FinishTime = 0,
                    IsReceive = true
                    }
                  },
                   new AchievementGroupInfo
                  {
                   AchievementGroupEntry = new AchievementGroupEntry
                    {
                    Id = 1002,
                    FinishTime = 0,
                    IsReceive = true
                    }
                  },
                   new AchievementGroupInfo
                  {
                   AchievementGroupEntry = new AchievementGroupEntry
                    {
                    Id = 7001,
                    FinishTime = 0,
                    IsReceive = true
                    }
                  },
                   new AchievementGroupInfo
                  {
                   AchievementGroupEntry = new AchievementGroupEntry
                    {
                    Id = 7002,
                    FinishTime = 0,
                    IsReceive = true
                    }
                  },
                   new AchievementGroupInfo
                   {
                     AchievementGroupEntry = new AchievementGroupEntry
                     {
                      Id = 7003,
                      FinishTime = 0,
                      IsReceive = true
                      }
                    },
                     new AchievementGroupInfo
                     {
                     AchievementGroupEntry = new AchievementGroupEntry
                     {
                      Id = 7004,
                      FinishTime = 0,
                      IsReceive = true
                      }
                    },
                     new AchievementGroupInfo
                     {
                     AchievementGroupEntry = new AchievementGroupEntry
                     {
                      Id = 2001,
                      FinishTime = 0,
                      IsReceive = true
                      }
                    },
                     new AchievementGroupInfo
                     {
                     AchievementGroupEntry = new AchievementGroupEntry
                     {
                      Id = 2003,
                      FinishTime = 0,
                      IsReceive = true
                      }
                    }
             }
        });
    }

    [NetEvent(MessageId.AchievementFinishRequest)]
    public RpcResult AchievementFinishRequest(AchievementFinishRequest request)
    {
        var itemid = request.Id;

        if (itemid != 0)
        {
            return Response(MessageId.AchievementFinishResponse, new AchievementFinishResponse
            {
                ErrorCode = 0
            });
        }
        else
        {
            return Response(MessageId.AchievementFinishResponse, new AchievementFinishResponse
            {
                ErrorCode = 3
            });
        }

    }

    [NetEvent(MessageId.AchievementReceiveRequest)]
    public RpcResult AchievementReceiveRequest(AchievementReceiveRequest request)
    {

        bool isGroupId = request.IsGroupId;
        int id = request.Id;
        return Response(MessageId.AchievementReceiveResponse, new AchievementReceiveResponse
        {
            ErrorCode = (int)ErrorCode.Success,
            ErrorParams = { "AchievementReceiveResponseSuccess?" },
            ItemMap = { { 300101, 3001 }, { 300102, 3001 } ,{ 1,100101}, { 2,100102} }

        });
    }

    //public async void GroupProgressNotify(GachaResponse response)
    //public async void ProgressNotify(GachaResponse response)
    //public async void ListProgressNotify(GachaResponse response)
}// AchievementController.



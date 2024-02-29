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
        //some id can't show  
        return Response(MessageId.AchievementInfoResponse, new AchievementInfoResponse
        {
            AchievementGroupInfoList = {
        new AchievementGroupInfo//1002
        {
            AchievementGroupEntry = new AchievementGroupEntry
            {
                Id = 1002,
                FinishTime = 0,
                IsReceive = true
            },
            AchievementEntryList =
            {
                new AchievementEntry
                {
                    Id = 100201,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100202,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100203,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100204,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100205,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100206,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100207,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100208,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100209,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100210,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100211,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100212,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100213,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100214,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100215,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100216,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                }
            }
        },//1002
        new AchievementGroupInfo
        {
            AchievementGroupEntry = new AchievementGroupEntry
            {
                Id = 1004,
                FinishTime = 0,
                IsReceive = true
            },
            AchievementEntryList =
            {
                new AchievementEntry
                {
                    Id = 100401,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100402,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100403,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100404,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100405,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100406,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100407,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100410,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100411,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100412,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100413,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100408,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100409,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100414,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100416,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100417,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100418,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100419,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100420,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100421,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100422,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100423,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100424,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100425,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100426,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100427,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100428,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100429,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100430,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                }
            }
        },//1004
        new AchievementGroupInfo
        {
            AchievementGroupEntry = new AchievementGroupEntry
            {
                Id = 2001,
                FinishTime = 0,
                IsReceive = true
            },
            AchievementEntryList =
            {
                new AchievementEntry
                {
                    Id = 200101,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 200102,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 200103,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 200104,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 200105,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 200106,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 200107,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 200108,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 200110,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 200111,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 200115,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                }
            }
        },//2001
        new AchievementGroupInfo
        {
            AchievementGroupEntry = new AchievementGroupEntry
            {
                Id = 2003,
                FinishTime = 0,
                IsReceive = true
            },
            AchievementEntryList =
            {
                new AchievementEntry
                {
                    Id = 200302,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 200303,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 200304,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 200305,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 200306,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                }
            }
        },//2003
        new AchievementGroupInfo{
            AchievementGroupEntry = new AchievementGroupEntry
            {
                Id = 3001,
                FinishTime = 0,
                IsReceive = true
            },
            AchievementEntryList = {
                new AchievementEntry{
                    Id = 300101,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress{
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry{
                    Id = 300102,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress{
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry{
                    Id = 300103,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress{
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry{
                    Id = 300104,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress{
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry{
                    Id = 300105,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress{
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry{
                    Id = 300106,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress{
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry{
                    Id = 300107,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress{
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry{
                    Id = 300108,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress{
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry{
                    Id = 300109,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress{
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry{
                    Id = 300110,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress{
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry{
                    Id = 300111,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress{
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry{
                    Id = 300112,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress{
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300113,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300114,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300115,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300116,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300117,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300118,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300119,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300120,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300121,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300122,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300124,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300510,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300523,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },//*/
            }
        },//3001
        new AchievementGroupInfo{
            AchievementGroupEntry = new AchievementGroupEntry
            {
                Id = 3002,
                FinishTime = 0,
                IsReceive = true
            },
            AchievementEntryList =
            {
                new AchievementEntry
                {
                    Id = 100110,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100111,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100112,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100113,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100114,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100115,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100116,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100117,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100118,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100119,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100120,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100121,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100122,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100123,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300205,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300206,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300207,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },

                new AchievementEntry
                {
                    Id = 300208,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300209,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300210,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300211,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300212,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300213,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                }
            }
        },//3002
        new AchievementGroupInfo
        {
            AchievementGroupEntry = new AchievementGroupEntry
            {
                Id = 3003,
                FinishTime = 0,
                IsReceive = true
            },
            AchievementEntryList =
            {
                new AchievementEntry
                {
                    Id = 300301,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300302,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300304,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300305,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300306,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300307,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300309,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300310,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                }
            }
        },//3003
        new AchievementGroupInfo
        {
            AchievementGroupEntry = new AchievementGroupEntry
            {
                Id = 3004,
                FinishTime = 0,
                IsReceive = true
            },
            AchievementEntryList =
            {
                new AchievementEntry
                {
                    Id = 300401,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300402,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300403,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300404,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300405,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300406,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300407,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300408,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300409,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300410,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300411,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                }
            }
        },//3004
        new AchievementGroupInfo
        {
            AchievementGroupEntry = new AchievementGroupEntry
            {
                Id = 3005,
                FinishTime = 0,
                IsReceive = true
            },
            AchievementEntryList =
            {
                new AchievementEntry
                {
                    Id = 300501,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300502,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300503,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300504,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300505,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300506,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300507,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300508,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300509,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300511,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300512,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300513,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 300514,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                }
            }
        },//3005
        new AchievementGroupInfo
        {
            AchievementGroupEntry = new AchievementGroupEntry
            {
                Id = 1001,
                FinishTime = 0,
                IsReceive = true
            },
            AchievementEntryList =
            {
                new AchievementEntry
                {
                    Id = 110101,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 110102,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 110103,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 110105,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100101,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100102,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100103,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100104,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100105,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100106,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100107,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 110106,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 110107,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100110,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100111,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100112,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100113,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100114,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100116,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100117,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100118,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100115,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 100108,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                }
            }
        },//1001


        new AchievementGroupInfo
        {
            AchievementGroupEntry = new AchievementGroupEntry
            {
                Id = 4001,
                FinishTime = 0,
                IsReceive = true
            },
            AchievementEntryList =
            {
                new AchievementEntry
                {
                    Id = 400101,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 400102,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 400103,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 400104,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 400108,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 400109,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 400110,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 400111,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 400112,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 400113,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 400114,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 400105,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 400106,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 400107,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 400115,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                }
            }
        },//4001
        new AchievementGroupInfo
        {
            AchievementGroupEntry = new AchievementGroupEntry
            {
                Id = 4002,
                FinishTime = 0,
                IsReceive = true
            },
            AchievementEntryList =
            {
                new AchievementEntry
                {
                    Id = 400201,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 400202,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 400203,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 400204,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 400206,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 400207,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 400208,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 400209,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 400213,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 400214,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 400205,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 400210,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 400211,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 400212,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                },
                new AchievementEntry
                {
                    Id = 400215,
                    FinishTime = 0,
                    IsReceive = true,
                    Progress = new AchievementProgress
                    {
                        CurProgress = 1,
                        TotalProgress = 1
                    }
                }
            }
        },//4002
       /* new AchievementGroupInfo
          {
              AchievementGroupEntry = new AchievementGroupEntry
              {
                  Id = 7001,
                  FinishTime = 0,
                  IsReceive = true
              },
              AchievementEntryList =
              {
                  new AchievementEntry
                  {
                      Id = 101700101,
                      FinishTime = 0,
                      IsReceive = true,
                      Progress = new AchievementProgress
                      {
                          CurProgress = 1,
                          TotalProgress = 1
                      }
                  },
                  new AchievementEntry
                  {
                      Id = 101700102,
                      FinishTime = 0,
                      IsReceive = true,
                      Progress = new AchievementProgress
                      {
                          CurProgress = 1,
                          TotalProgress = 1
                      }
                  },
                  new AchievementEntry
                  {
                      Id = 101700103,
                      FinishTime = 0,
                      IsReceive = true,
                      Progress = new AchievementProgress
                      {
                          CurProgress = 1,
                          TotalProgress = 1
                      }
                  },
                  new AchievementEntry
                  {
                      Id = 101700105,
                      FinishTime = 0,
                      IsReceive = true,
                      Progress = new AchievementProgress
                      {
                          CurProgress = 1,
                          TotalProgress = 1
                      }
                  }
              }
          },//7001*/
        /*new AchievementGroupInfo
          {
              AchievementGroupEntry = new AchievementGroupEntry
              {
                  Id = 7002,
                  FinishTime = 0,
                  IsReceive = true
              },
              AchievementEntryList =
              {
                  new AchievementEntry
                  {
                      Id = 101700202,
                      FinishTime = 0,
                      IsReceive = true,
                      Progress = new AchievementProgress
                      {
                          CurProgress = 1,
                          TotalProgress = 1
                      }
                  },
                  new AchievementEntry
                  {
                      Id = 101700203,
                      FinishTime = 0,
                      IsReceive = true,
                      Progress = new AchievementProgress
                      {
                          CurProgress = 1,
                          TotalProgress = 1
                      }
                  },
                  new AchievementEntry
                  {
                      Id = 101700206,
                      FinishTime = 0,
                      IsReceive = true,
                      Progress = new AchievementProgress
                      {
                          CurProgress = 1,
                          TotalProgress = 1
                      }
                  }
              }
          },//7002*/
        /*new AchievementGroupInfo
          {
              AchievementGroupEntry = new AchievementGroupEntry
              {
                  Id = 7003,
                  FinishTime = 0,
                  IsReceive = true
              },
              AchievementEntryList =
              {
                  new AchievementEntry
                  {
                      Id = 101700301,
                      FinishTime = 0,
                      IsReceive = true,
                      Progress = new AchievementProgress
                      {
                          CurProgress = 1,
                          TotalProgress = 1
                      }
                  },
                  new AchievementEntry
                  {
                      Id = 101700302,
                      FinishTime = 0,
                      IsReceive = true,
                      Progress = new AchievementProgress
                      {
                          CurProgress = 1,
                          TotalProgress = 1
                      }
                  },
                  new AchievementEntry
                  {
                      Id = 101700303,
                      FinishTime = 0,
                      IsReceive = true,
                      Progress = new AchievementProgress
                      {
                          CurProgress = 1,
                          TotalProgress = 1
                      }
                  },
                  new AchievementEntry
                  {
                      Id = 101700304,
                      FinishTime = 0,
                      IsReceive = true,
                      Progress = new AchievementProgress
                      {
                          CurProgress = 1,
                          TotalProgress = 1
                      }
                  },
                  new AchievementEntry
                  {
                      Id = 101700305,
                      FinishTime = 0,
                      IsReceive = true,
                      Progress = new AchievementProgress
                      {
                          CurProgress = 1,
                          TotalProgress = 1
                      }
                  },
                  new AchievementEntry
                  {
                      Id = 101700306,
                      FinishTime = 0,
                      IsReceive = true,
                      Progress = new AchievementProgress
                      {
                          CurProgress = 1,
                          TotalProgress = 1
                      }
                  },
                  new AchievementEntry
                  {
                      Id = 101700307,
                      FinishTime = 0,
                      IsReceive = true,
                      Progress = new AchievementProgress
                      {
                          CurProgress = 1,
                          TotalProgress = 1
                      }
                  },
                  new AchievementEntry
                  {
                      Id = 101700308,
                      FinishTime = 0,
                      IsReceive = true,
                      Progress = new AchievementProgress
                      {
                          CurProgress = 1,
                          TotalProgress = 1
                      }
                  }
              }
          },//7003*/
        /*new AchievementGroupInfo
          {
              AchievementGroupEntry = new AchievementGroupEntry
              {
                  Id = 7004,
                  FinishTime = 0,
                  IsReceive = true
              },
              AchievementEntryList =
              {
                  new AchievementEntry
                  {
                      Id = 101700402,
                      FinishTime = 0,
                      IsReceive = true,
                      Progress = new AchievementProgress
                      {
                          CurProgress = 1,
                          TotalProgress = 1
                      }
                  },
                  new AchievementEntry
                  {
                      Id = 101700403,
                      FinishTime = 0,
                      IsReceive = true,
                      Progress = new AchievementProgress
                      {
                          CurProgress = 1,
                          TotalProgress = 1
                      }
                  },
                  new AchievementEntry
                  {
                      Id = 101700406,
                      FinishTime = 0,
                      IsReceive = true,
                      Progress = new AchievementProgress
                      {
                          CurProgress = 1,
                          TotalProgress = 1
                      }
                  }
              }
          }, //7004*/

     }
        });;
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

        //bool isGroupId = request.IsGroupId;
        //int id = request.Id;
        return Response(MessageId.AchievementReceiveResponse, new AchievementReceiveResponse
        {
            ErrorCode = (int)ErrorCode.Success,
            ErrorParams = { "AchievementReceiveResponseSuccess?" },
            ItemMap = { }

        });
    }

    //public async void AchievementGroupProgressNotify()
    //public async void AchievementProgressNotify()
    //public async void AchievementListProgressNotify()
}// AchievementController.



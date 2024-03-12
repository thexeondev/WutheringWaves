using GameServer.Controllers.Attributes;
using GameServer.Controllers.Gacha;
using GameServer.Network;
using GameServer.Settings;
using Protocol;

namespace GameServer.Controllers;
internal class GachaController : Controller
{
    public GachaController(PlayerSession session) : base(session)
    {
        // GachaController.
    }

    private int rolePoolId = 100001;
    private int weaponPoolId = 200001;
    private int basePoolId = 31;
    private int itemIncrId;
    private readonly string gachaurlHost = (string)DBManager.GetMember("data/gameplay.json", "GachaUrl.Host")!;
    private readonly string gachaurlPort = (string)DBManager.GetMember("data/gameplay.json", "GachaUrl.Port")!;

    [NetEvent(MessageId.GachaUsePoolRequest)]
    public RpcResult OnGachaUsePoolRequest(GachaUsePoolRequest request)
    {
        GachaUsePoolResponse response = new()
        {
            ErrorCode = (int)ErrorCode.Success
        };
        switch (request.GachaId)
        {
            case 100001:
                rolePoolId = request.PoolId;
                break;
            case 200001:
                weaponPoolId = request.PoolId;
                break;
            case 31:
                basePoolId = request.PoolId;
                break;
            default:
                break;
        }
        return Response(MessageId.GachaUsePoolResponse, response);
    }

    [NetEvent(MessageId.GachaInfoRequest)]
    public RpcResult OnGachaInfoRequest()
    {
        return Response(MessageId.GachaInfoResponse, new GachaInfoResponse()
        {
            DailyTotalLeftTimes = 5000,
            ErrorCode = 0,
            RecordId = "eblan",
            GachaInfos =
            {
                new GachaInfo
                {
                    Id = 1,
                    ItemId = 50001,
                    GachaConsumes =
                    {
                        new GachaConsume
                        {
                            Times = 10,
                            Consume = 8
                        },
                    },
                    UsePoolId = 1,
                    Pools =
                    {
                        new GachaPoolInfo
                        {
                            Id = 1,
                            BeginTime = 0,
                            EndTime = 25569600,
                            Sort = 1,
                            Urls = { $"http://{gachaurlHost}:{gachaurlPort}/Gacha/newplayer.json" }
                        },
                    },
                    BeginTime = 0,
                    EndTime = 25569600,
                    DailyLimitTimes = 1000,
                    TotalLimitTimes = 9000,
                    ResourcesId = "UiItem_NewPlayerGachaPool",
                    Sort = 1,
                    GroupId = 8,
                },
                new GachaInfo
                {
                    Id = 100001,
                    TodayTimes = 0,
                    TotalTimes = 0,
                    ItemId = 50002,
                    GachaConsumes =
                    {
                        new GachaConsume
                        {
                            Times = 1,
                            Consume = 1
                        },
                        new GachaConsume
                        {
                            Times = 10,
                            Consume = 10
                        },
                    },
                    UsePoolId = rolePoolId,
                    Pools =
                    {
                        new GachaPoolInfo
                        {
                            Id = 100001,
                            BeginTime = 0,
                            EndTime = 25569600,
                            Sort = 2,
                            Urls ={ $"http://{gachaurlHost}:{gachaurlPort}/Gacha/roleup1.json" }
                        },
                        new GachaPoolInfo
                        {
                            Id = 100002,
                            BeginTime = 0,
                            EndTime = 25569600,
                            Sort = 2,
                            Urls ={ $"http://{gachaurlHost}:{gachaurlPort}/Gacha/roleup2.json" }
                        },
                    },
                    BeginTime = 0,
                    EndTime = 25569600,
                    DailyLimitTimes = 1000,
                    TotalLimitTimes = 9000,
                    ResourcesId = "UiItem_RoleUpGachaPool",
                    Sort = 2,
                    GroupId = 8,
                },
                new GachaInfo
                {
                    Id = 200001,
                    TodayTimes = 0,
                    TotalTimes = 0,
                    ItemId = 50005,
                    GachaConsumes =
                    {
                        new GachaConsume
                        {
                            Times = 1,
                            Consume = 1
                        },
                        new GachaConsume
                        {
                            Times = 10,
                            Consume = 10
                        },
                    },
                    UsePoolId = weaponPoolId,
                    Pools =
                    {
                        new GachaPoolInfo
                        {
                            Id = 200001,
                            BeginTime = 0,
                            EndTime = 25569600,
                            Sort = 3,
                            Urls ={ $"http://{gachaurlHost}:{gachaurlPort}/Gacha/weaponup1.json" }
                        },
                        new GachaPoolInfo
                        {
                            Id = 200002,
                            BeginTime = 0,
                            EndTime = 25569600,
                            Sort = 3,
                            Urls ={ $"http://{gachaurlHost}:{gachaurlPort}/Gacha/weaponup2.json" }
                        },
                    },
                    BeginTime = 0,
                    EndTime = 25569600,
                    DailyLimitTimes = 1000,
                    TotalLimitTimes = 9000,
                    ResourcesId = "UiItem_WeaponGachaPool",
                    Sort = 3,
                    GroupId = 8,
                },
                new GachaInfo
                {
                    Id = 2,
                    TodayTimes = 0,
                    TotalTimes = 0,
                    ItemId = 50001,
                    GachaConsumes =
                    {
                        new GachaConsume
                        {
                            Times = 1,
                            Consume = 1
                        },
                        new GachaConsume
                        {
                            Times = 10,
                            Consume = 10
                        },
                    },
                    UsePoolId = 2,
                    Pools =
                    {
                        new GachaPoolInfo
                        {
                            Id = 2,
                            BeginTime = 0,
                            EndTime = 25569600,
                            Sort = 4,
                            Urls = { $"http://{gachaurlHost}:{gachaurlPort}/Gacha/baserole.json" }
                        }
                    },
                    BeginTime = 0,
                    EndTime = 25569600,
                    DailyLimitTimes = 1000,
                    TotalLimitTimes = 9000,
                    ResourcesId = "UiItem_BaseGachaPool",
                    Sort = 4,
                    GroupId = 8,
                },
                new GachaInfo
                {
                    Id = 31,
                    TodayTimes = 0,
                    TotalTimes = 0,
                    ItemId = 50001,
                    GachaConsumes =
                    {
                        new GachaConsume
                        {
                            Times = 1,
                            Consume = 1
                        },
                        new GachaConsume
                        {
                            Times = 10,
                            Consume = 10
                        },
                    },
                    UsePoolId = basePoolId,
                    Pools =
                    {
                        new GachaPoolInfo
                        {
                            Id = 31,
                            BeginTime = 0,
                            EndTime = 25569600,
                            Sort = 5,
                            Urls ={ $"http://{gachaurlHost}:{gachaurlPort}/Gacha/baseweapon1.json" }
                        },
                        new GachaPoolInfo
                        {
                            Id = 32,
                            BeginTime = 0,
                            EndTime = 25569600,
                            Sort = 5,
                            Urls = { $"http://{gachaurlHost}:{gachaurlPort}/Gacha/baseweapon2.json" }
                        },
                        new GachaPoolInfo
                        {
                            Id = 33,
                            BeginTime = 0,
                            EndTime = 25569600,
                            Sort = 5,
                            Urls ={ $"http://{gachaurlHost}:{gachaurlPort}/Gacha/baseweapon3.json" }
                        },
                        new GachaPoolInfo
                        {
                            Id = 34,
                            BeginTime = 0,
                            EndTime = 25569600,
                            Sort = 5,
                            Urls = { $"http://{gachaurlHost}:{gachaurlPort}/Gacha/baseweapon4.json" }
                        },
                        new GachaPoolInfo
                        {
                            Id = 35,
                            BeginTime = 0,
                            EndTime = 25569600,
                            Sort = 5,
                            Urls = { $"http://{gachaurlHost}:{gachaurlPort}/Gacha/baseweapon5.json" }
                        },
                    },
                    BeginTime = 0,
                    EndTime = 25569600,
                    DailyLimitTimes = 1000,
                    TotalLimitTimes = 9000,
                    ResourcesId = "UiItem_BaseGachaPool",
                    Sort = 5,
                    GroupId = 8,
                },
            }
        });
    }

    [NetEvent(MessageId.GachaRequest)]
    public RpcResult OnGachaRequest(GachaRequest request, PoolInfo poolInfo)
    {
        GachaResponse response = new()
        {
            ErrorCode = (int)ErrorCode.Success
        };

        switch (request.GachaId)
        {
            case 1:
                for (int i = 0; i < request.GachaTimes; i++)
                {
                    (int, int) gachaResult = poolInfo.BaseRole.DoPull();
                    AddGachaResult(response, gachaResult);
                }
                break;
            case 100001:
                if (rolePoolId == 100001)
                {
                    for (int i = 0; i < request.GachaTimes; i++)
                    {
                        (int, int) gachaResult = poolInfo.RoleUp1.DoPull();
                        AddGachaResult(response, gachaResult);
                    }
                }
                else
                {
                    for (int i = 0; i < request.GachaTimes; i++)
                    {
                        (int, int) gachaResult = poolInfo.RoleUp2.DoPull();
                        AddGachaResult(response, gachaResult);
                    }
                }
                break;
            case 200001:
                if (weaponPoolId == 200001)
                {
                    for (int i = 0; i < request.GachaTimes; i++)
                    {
                        (int, int) gachaResult = poolInfo.WeaponUp1.DoPull();
                        AddGachaResult(response, gachaResult);
                    }
                }
                else
                {
                    for (int i = 0; i < request.GachaTimes; i++)
                    {
                        (int, int) gachaResult = poolInfo.WeaponUp2.DoPull();
                        AddGachaResult(response, gachaResult);
                    }
                }
                break;
            case 2:
                for (int i = 0; i < request.GachaTimes; i++)
                {
                    (int, int) gachaResult = poolInfo.BaseRole.DoPull();
                    AddGachaResult(response, gachaResult);
                }
                break;
            case 31:
                if (basePoolId == 31)
                {
                    for (int i = 0; i < request.GachaTimes; i++)
                    {
                        (int, int) gachaResult = poolInfo.BaseWeapon1.DoPull();
                        AddGachaResult(response, gachaResult);
                    }
                }
                else if (basePoolId == 32)
                {
                    for (int i = 0; i < request.GachaTimes; i++)
                    {
                        (int, int) gachaResult = poolInfo.BaseWeapon2.DoPull();
                        AddGachaResult(response, gachaResult);
                    }
                }
                else if (basePoolId == 33)
                {
                    for (int i = 0; i < request.GachaTimes; i++)
                    {
                        (int, int) gachaResult = poolInfo.BaseWeapon3.DoPull();
                        AddGachaResult(response, gachaResult);
                    }
                }
                else if (basePoolId == 34)
                {
                    for (int i = 0; i < request.GachaTimes; i++)
                    {
                        (int, int) gachaResult = poolInfo.BaseWeapon4.DoPull();
                        AddGachaResult(response, gachaResult);
                    }
                }
                else if (basePoolId == 35)
                {
                    for (int i = 0; i < request.GachaTimes; i++)
                    {
                        (int, int) gachaResult = poolInfo.BaseWeapon5.DoPull();
                        AddGachaResult(response, gachaResult);
                    }
                }
                break;
        }
        return Response(MessageId.GachaResponse, response);
    }

    public async void AddGachaResult(GachaResponse response, (int, int) gachaResult)
    {
        response.GachaResults.Add(new GachaResult()
        {
            GachaReward = new GachaReward
            {
                ItemId = gachaResult.Item1,
                ItemCount = 1
            },
            ExtraRewards =
            {
                new GachaReward { ItemId = 50003, ItemCount = gachaResult.Item2 == 3 ? 15 : 0 },
                new GachaReward { ItemId = 50004, ItemCount = gachaResult.Item2 == 5 ? 15 : (gachaResult.Item2 == 4 ? 3 : 0) }
            },
        });

        NormalItemAddNotify itemAddNotify = new();
        if (gachaResult.Item1.ToString().Length == 4)
        {
            itemAddNotify.NormalItemList.Add(new NormalItem() { Id = 10000000 + gachaResult.Item1, Count = 1 });
        }
        await Session.Push(MessageId.NormalItemAddNotify, itemAddNotify);

        WeaponItemAddNotify weaponAddNotify = new();
        if (gachaResult.Item1.ToString().Length == 8)
        {
            weaponAddNotify.WeaponItemList.Add(new WeaponItem()
            {
                Id = gachaResult.Item1,
                IncrId = ++itemIncrId,
                WeaponLevel = 1,
                WeaponResonLevel = 1
            });
        }
        await Session.Push(MessageId.WeaponItemAddNotify, weaponAddNotify);
    }
}

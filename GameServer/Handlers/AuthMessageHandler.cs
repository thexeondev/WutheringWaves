using GameServer.Handlers.Attributes;
using GameServer.Network;
using Protocol;

namespace GameServer.Handlers;
internal class AuthMessageHandler : MessageHandlerBase
{
    private const int CharacterId = 1601;

    public AuthMessageHandler(KcpSession session) : base(session)
    {
        // AuthMessageHandler.
    }

    [MessageHandler(MessageId.LoginRequest)]
    public async Task OnLoginRequest(ReadOnlyMemory<byte> data)
    {
        LoginRequest request = LoginRequest.Parser.ParseFrom(data.Span);

        Console.WriteLine(request);

        await Session.Rpc.ReturnAsync(MessageId.LoginResponse, new LoginResponse
        {
            Code = 0,
            Platform = "PC",
            Timestamp = DateTimeOffset.Now.ToUnixTimeSeconds()
        });
    }

    [MessageHandler(MessageId.EnterGameRequest)]
    public async Task OnEnterGameRequest(ReadOnlyMemory<byte> data)
    {
        EnterGameRequest request = EnterGameRequest.Parser.ParseFrom(data.Span);
        Console.WriteLine(request);

        await Session.PushMessage(MessageId.BasicInfoNotify, new BasicInfoNotify
        {
            RandomSeed = 1337,
            Id = 1337,
            Birthday = 0,
            Attributes =
            {
                new PlayerAttr
                {
                    Key = (int)PlayerAttrKey.Name,
                    ValueType = (int)PlayerAttrType.String,
                    StringValue = "ReversedRooms"
                },
                new PlayerAttr
                {
                    Key = (int)PlayerAttrKey.Level,
                    ValueType = (int)PlayerAttrType.Int32,
                    Int32Value = 10
                }
            },
            RoleShowList =
            {
                new RoleShowEntry
                {
                    Level = 1,
                    RoleId = CharacterId
                }
            },
        });

        int[] quests = [10010001, 10010002, 20020010, 20017001, 10010003, 10010004, 10010005, 10010006, 10010007, 20010001, 20010002, 20040001, 10010008, 10010009, 10010010, 10010011, 20010003, 20040002, 20021501, 20020004, 20010004, 20040003, 20040004, 20030001, 20032004, 20010005, 20040005, 20040006, 20040007, 20160001, 20160002, 20010007, 20010008, 20012003, 20010006, 20042301, 20032005, 20032006, 20032007, 20032008, 20032009, 59990001, 59990002, 59990003, 59990004, 50010001, 50010002, 50010003, 50010004, 50010005, 50010006, 50010007, 50020001, 50020002, 50020003, 50020004, 50020005, 50020006, 50020007, 50030001, 50030002, 50030003, 50030004, 50030005, 50030006, 50030007, 50040001, 50040002, 50040003, 50040004, 50000001, 50000002, 50000003, 50000004, 50000005, 50000006, 50000007, 50000008, 50050001, 50050002, 50050003, 50050004, 50060001, 50060002, 50060003, 50060004];
        QuestListNotify questList = new();

        foreach (int id in quests)
        {
            questList.Quests.Add(new QuestInfo
            {
                Status = 3,
                QuestId = id,
            });
        }

        int[] functions = [10001, 10002, 10003, 10004, 10005, 10006, 10007, 10008, 10009, 10010, 10011, 10012, 10013, 10014, 10015, 10016, 10017, 10018, 10019, 10020, 10021, 10022, 10023, 10024, 10025, 10026, 10027, 10028, 10029, 10030, 10031, 10033, 10034, 10035, 10036, 10041, 10042, 10043, 10046, 10047, 10048, 10049, 10050, 10052, 10023001, 10023002, 10023004, 10023005, 10053, 10054, 10001003, 10055, 10026001, 10026002, 10026003, 10026004, 10026005, 10026006, 10026008, 10056, 10026101, 110057, 10001004, 10037, 10057, 10059, 10058, 10023003, 10032, 110056, 110058, 10060, 10061];
        FuncOpenNotify funcOpen = new();

        foreach (int id in functions)
        {
            funcOpen.Func.Add(new Function
            {
                Id = id,
                Flag = 2
            });
        }

        await Session.PushMessage(MessageId.FuncOpenNotify, funcOpen);

        await Session.PushMessage(MessageId.QuestListNotify, questList);

        await Session.PushMessage(MessageId.PbGetRoleListNotify, new PbGetRoleListNotify
        {
            RoleList =
            {
                new roleInfo
                {
                    RoleId = 1601,
                    Level = 1,
                }
            }
        });

        await Session.PushMessage(MessageId.JoinSceneNotify, new JoinSceneNotify
        {
            MaxEntityId = 2,
            TransitionOption = new TransitionOptionPb
            {
                TransitionType = (int)TransitionType.Empty
            },
            SceneInfo = new SceneInformation
            {
                OwnerId = 1337,
                Mode = (int)SceneMode.Single,
                InstanceId = 8,
                AoiData = new PlayerSceneAoiData
                {
                    GenIds = {1},
                    Entities =
                    {
                        new EntityPb
                        {
                            EntityState = (int)EntityState.Born,
                            EntityType = (int)EEntityType.Player,
                            PlayerId = 1337,
                            LivingStatus = (int)LivingStatus.Alive,
                            ConfigId = CharacterId,
                            ConfigType = (int)EntityConfigType.Character,
                            Id = 1,
                            IsVisible = true,
                            Pos = new Vector
                            {
                                X = 4000,
                                Y = -2000,
                                Z = 260
                            },
                            Rot = new(),
                            InitLinearVelocity = new(),
                            PrefabIncId = 0,
                            InitPos = new Vector
                            {
                                X = 4000,
                                Y = -2000,
                                Z = 260
                            },
                            ComponentPbs =
                            {
                                new EntityComponentPb
                                {
                                    VisionSkillComponent = new VisionSkillComponentPb
                                    {
                                        VisionSkillInfos =
                                        {
                                            new VisionSkillInformation
                                            {
                                                SkillId = 1302001,
                                                Level = 1,
                                                Quality = 1,
                                            }
                                        }
                                    },
                                },
                                new EntityComponentPb
                                {
                                    AttributeComponent = new AttributeComponentPb
                                    {
                                        GameAttributes =
                                        {
                                            new GameplayAttributeData
                                            {
                                                AttributeType = (int)EAttributeType.Life,
                                                BaseValue = 1000,
                                                CurrentValue = 1000
                                            },
                                            new GameplayAttributeData
                                            {
                                                AttributeType = (int)EAttributeType.LifeMax,
                                                BaseValue = 1000,
                                                CurrentValue = 1000
                                            },
                                            new GameplayAttributeData
                                            {
                                                AttributeType = (int)EAttributeType.EnergyMax,
                                                BaseValue = 10,
                                                CurrentValue = 10
                                            },
                                            new GameplayAttributeData
                                            {
                                                AttributeType = (int)EAttributeType.Energy,
                                                BaseValue = 10,
                                                CurrentValue = 10
                                            },
                                            new GameplayAttributeData
                                            {
                                                AttributeType = (int)EAttributeType.SpecialEnergy3,
                                                BaseValue = 10,
                                                CurrentValue = 10
                                            },
                                            new GameplayAttributeData
                                            {
                                                AttributeType = (int)EAttributeType.SpecialEnergy3Max,
                                                BaseValue = 10,
                                                CurrentValue = 10
                                            },
                                            new GameplayAttributeData
                                            {
                                                AttributeType = (int)EAttributeType.AutoAttackSpeed,
                                                BaseValue = 10000,
                                                CurrentValue = 10000
                                            },
                                            new GameplayAttributeData
                                            {
                                                AttributeType = (int)EAttributeType.CastAttackSpeed,
                                                BaseValue = 10000,
                                                CurrentValue = 10000
                                            },
                                            new GameplayAttributeData
                                            {
                                                AttributeType = (int)EAttributeType.Atk,
                                                BaseValue = 1,
                                                CurrentValue = 1
                                            },
                                            new GameplayAttributeData
                                            {
                                                AttributeType = (int)EAttributeType.Lv,
                                                BaseValue = 1,
                                                CurrentValue = 1
                                            },
                                        },
                                    }
                                },
                                new EntityComponentPb
                                {
                                    ConcomitantsComponentPb = new ConcomitantsComponentPb
                                    {
                                        CustomEntityIds = {1},
                                    },
                                }
                            },
                        }
                    }
                },
                TimeInfo = new SceneTimeInfo
                {
                    Hour = 23
                },
                PlayerInfos = 
                {
                    new ScenePlayerInformation
                    {
                        PlayerId = 1337,
                        Level = 1,
                        IsOffline = false,
                        Location = new()
                        {
                            X = 4000,
                            Y = -2000,
                            Z = 260
                        },
                        FightRoleInfos =
                        {
                            new FightRoleInformation
                            {
                                EntityId = 1,
                                CurHp = 1000,
                                MaxHp = 1000,
                                IsControl = true,
                                RoleId = CharacterId,
                                RoleLevel = 1,
                            }
                        },
                        PlayerName = "ReversedRooms"
                    }
                },
                CurContextId = 1337
            }
        });

        await Session.PushMessage(MessageId.PushDataCompleteNotify, new PushDataCompleteNotify());

        await Session.Rpc.ReturnAsync(MessageId.EnterGameResponse, new EnterGameResponse());
    }

    [MessageHandler(MessageId.TutorialInfoRequest)]
    public async Task OnTutorialInfoRequest(ReadOnlyMemory<byte> data)
    {
        int[] tutorials = [30001, 30002, 30003, 30004, 30005, 30006, 30007, 30011, 30012, 30008, 30009, 30010, 30013, 30014, 30015, 30016, 30017, 30018, 30019, 30020, 30021, 30022, 30023, 30024, 40001, 30025, 30026, 30027, 30028, 30029, 30030, 30031, 30032, 30033, 30034, 30035, 30036, 50001, 50002, 50003, 50004, 50005, 50006, 50007, 50008, 50009, 50010, 50011, 33001, 34017, 34018, 32001, 32002, 32003, 32004, 32005, 32006, 32007, 32008, 32009, 32010, 32011, 32012, 32013, 32014, 32015, 32016, 32017, 32018, 32019, 32020, 32021, 33002, 33003, 33004, 33005, 34001, 34002, 34003, 34004, 34005, 34006, 34007, 34008, 34009, 34010, 34011, 34012, 34013, 34014, 34015, 34016, 34019, 34020, 34021, 34022, 34023, 34024, 34025, 34027, 34028, 34029, 34030, 34031, 34032, 34033];
        TutorialInfoResponse rsp = new();
        foreach (int id in tutorials)
        {
            rsp.UnLockList.Add(new TutorialInfo
            {
                Id = id,
                GetAward = true,
                CreateTime = 1337
            });
        }

        await Session.Rpc.ReturnAsync(MessageId.TutorialInfoResponse, rsp);
    }

    [MessageHandler(MessageId.GetDetectionLabelInfoRequest)]
    public async Task OnGetDetectionLabelInfoRequest(ReadOnlyMemory<byte> _)
    {
        int[] guides = [0, 1, 2, 3, 14, 15, 16, 4, 21, 22, 7, 5, 18, 6, 61, 8, 9, 10, 11, 12, 13, 17, 19];
        int[] detectionTexts = [1, 2, 3, 4, 5, 6, 7, 0, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 21, 22, 61];

        GetDetectionLabelInfoResponse rsp = new() { UnlockLabelInfo = new() };
        rsp.UnlockLabelInfo.UnlockedGuideIds.AddRange(guides);
        rsp.UnlockLabelInfo.UnlockedDetectionTextIds.AddRange(detectionTexts);

        await Session.Rpc.ReturnAsync(MessageId.GetDetectionLabelInfoResponse, rsp);
    }

    [MessageHandler(MessageId.EntityOnLandedRequest)]
    public async Task OnEntityOnLandedRequest(ReadOnlyMemory<byte> data)
    {
        await Session.Rpc.ReturnAsync(MessageId.EntityOnLandedResponse, new EntityOnLandedResponse
        {

        });
    }

    [MessageHandler(MessageId.GachaInfoRequest)]
    public async Task OnGachaInfoRequest(ReadOnlyMemory<byte> data)
    {
        await Session.Rpc.ReturnAsync(MessageId.GachaInfoResponse, new GachaInfoResponse());
    }

    [MessageHandler(MessageId.RoleFavorListRequest)]
    public async Task OnRoleFavorListRequest(ReadOnlyMemory<byte> data)
    {
        await Session.Rpc.ReturnAsync(MessageId.RoleFavorListResponse, new RoleFavorListResponse());
    }

    [MessageHandler(MessageId.NormalItemRequest)]
    public async Task OnNormalItemRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.NormalItemResponse, new NormalItemResponse());
    }

    [MessageHandler(MessageId.WeaponItemRequest)]
    public async Task OnWeaponItemRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.WeaponItemResponse, new WeaponItemResponse());
    }

    [MessageHandler(MessageId.PhantomItemRequest)]
    public async Task OnPhantomItemRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.PhantomItemResponse, new PhantomItemResponse());
    }

    [MessageHandler(MessageId.FriendAllRequest)]
    public async Task OnFriendAllRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.FriendAllResponse, new FriendAllResponse());
    }

    [MessageHandler(MessageId.ItemExchangeInfoRequest)]
    public async Task OnItemExchangeInfoRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.ItemExchangeInfoResponse, new ItemExchangeInfoResponse());
    }

    [MessageHandler(MessageId.TowerChallengeRequest)]
    public async Task OnTowerChallengeRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.TowerChallengeResponse, new TowerChallengeResponse());
    }

    [MessageHandler(MessageId.InfluenceInfoRequest)]
    public async Task OnInfluenceInfoRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.InfluenceInfoResponse, new InfluenceInfoResponse());
    }

    [MessageHandler(MessageId.CycleTowerChallengeRequest)]
    public async Task OnCycleTowerChallengeRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.CycleTowerChallengeResponse, new CycleTowerChallengeResponse());
    }

    [MessageHandler(MessageId.AchievementInfoRequest)]
    public async Task OnAchievementInfoRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.AchievementInfoResponse, new AchievementInfoResponse());
    }

    [MessageHandler(MessageId.ActivityRequest)]
    public async Task OnActivityRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.ActivityResponse, new ActivityResponse());
    }

    [MessageHandler(MessageId.ExchangeRewardInfoRequest)]
    public async Task OnExchangeRewardInfoRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.ExchangeRewardInfoResponse, new ExchangeRewardInfoResponse());
    }

    [MessageHandler(MessageId.RoguelikeSeasonDataRequest)]
    public async Task OnRoguelikeSeasonDataRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.RoguelikeSeasonDataResponse, new RoguelikeSeasonDataResponse());
    }

    [MessageHandler(MessageId.MapTraceInfoRequest)]
    public async Task OnMapTraceInfoRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.MapTraceInfoResponse, new MapTraceInfoResponse());
    }

    [MessageHandler(MessageId.PayShopInfoRequest)]
    public async Task OnPayShopInfoRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.PayShopInfoResponse, new PayShopInfoResponse());
    }

    [MessageHandler(MessageId.LivenessRequest)]
    public async Task OnLivenessRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.LivenessResponse, new LivenessResponse());
    }

    [MessageHandler(MessageId.LordGymInfoRequest)]
    public async Task OnLordGymInfoRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.LordGymInfoResponse, new LordGymInfoResponse());
    }

    [MessageHandler(MessageId.UpdateSceneDateRequest)]
    public async Task OnUpdateSceneDateRequest(ReadOnlyMemory<byte> data)
    {
        await Session.Rpc.ReturnAsync(MessageId.UpdateSceneDateResponse, new UpdateSceneDateResponse());
    }

    [MessageHandler(MessageId.PlayerMotionRequest)]
    public async Task OnPlayerMotionRequest(ReadOnlyMemory<byte> data)
    {
        PlayerMotionRequest request = PlayerMotionRequest.Parser.ParseFrom(data.Span);
        await Session.Rpc.ReturnAsync(MessageId.PlayerMotionResponse, new PlayerMotionResponse
        {
            ErrorId = 0
        });
    }

    [MessageHandler(MessageId.EntityActiveRequest)]
    public async Task OnEntityActiveRequest(ReadOnlyMemory<byte> data)
    {
        EntityActiveRequest request = EntityActiveRequest.Parser.ParseFrom(data.Span);
        await Session.Rpc.ReturnAsync(MessageId.EntityActiveResponse, new EntityActiveResponse
        {
            ComponentPbs = { },
        });
    }

    [MessageHandler(MessageId.GetFormationDataRequest)]
    public async Task OnGetFormationDataRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.GetFormationDataResponse, new GetFormationDataResponse
        {
            Formations =
            {
                new FightFormation
                {
                    CurRole = CharacterId,
                    FormationId = 1,
                    IsCurrent = true,
                    RoleIds = { CharacterId },
                }
            },
        });
    }

    [MessageHandler(MessageId.EntityLoadCompleteRequest)]
    public async Task OnEntityLoadCompleteRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.EntityLoadCompleteResponse, new EntityLoadCompleteResponse());
    }

    [MessageHandler(MessageId.SceneLoadingFinishRequest)]
    public async Task OnSceneLoadingFinishRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.SceneLoadingFinishResponse, new SceneLoadingFinishResponse
        {
            ErrorCode = 0
        });
    }

    [MessageHandler(MessageId.HeartbeatRequest)]
    public async Task OnHeartbeatRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.HeartbeatResponse, new HeartbeatResponse());
    }

    [MessageHandler(MessageId.GuideInfoRequest)]
    public async Task OnGuideInfoRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.GuideInfoResponse, new GuideInfoResponse()
        {
            GuideGroupFinishList = { 60001, 60002, 60003, 60004, 60005, 60006, 60007, 60008, 60009, 60010, 60011, 60012, 60013, 60014, 60015, 60016, 60017, 60018, 60019, 60020, 60021, 60101, 60102, 60103, 62002, 62004, 62005, 62006, 62007, 62009, 62010, 62011, 62012, 62013, 62014, 62015, 62016, 62017, 62022, 62027, 62028, 62029, 62030, 62031, 62032, 62033, 62034, 62036, 65001, 67001, 67002, 67003, 67004, 67005, 67006, 67007, 67008, 67009, 67010, 67011, 67012, 67013, 67014, 67015, 67016, 67017, 67018, 67019, 67022, 62001, 62008, 62018, 62019, 62020, 62021, 62023, 62024, 62025, 62026, 62035, 65002, 65003, 65004, 65005 }
        });
    }
}

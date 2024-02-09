using GameServer.Handlers.Attributes;
using GameServer.Models;
using GameServer.Network;
using Protocol;

namespace GameServer.Handlers;
internal class AuthMessageHandler : MessageHandlerBase
{
    private readonly ModelManager _modelManager;

    public AuthMessageHandler(KcpSession session, ModelManager modelManager) : base(session)
    {
        _modelManager = modelManager;
    }

    [MessageHandler(MessageId.LoginRequest)]
    public async Task OnLoginRequest(ReadOnlyMemory<byte> _)
    {
        _modelManager.OnLogin();

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

        await Session.PushMessage(MessageId.BasicInfoNotify, new BasicInfoNotify
        {
            RandomSeed = 1337,
            Id = _modelManager.Player.Id,
            Birthday = 0,
            Attributes =
            {
                new PlayerAttr
                {
                    Key = (int)PlayerAttrKey.Name,
                    ValueType = (int)PlayerAttrType.String,
                    StringValue = _modelManager.Player.Name
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
                    RoleId = _modelManager.Player.CharacterId
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
                    RoleId = _modelManager.Player.CharacterId,
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
                            PlayerId = _modelManager.Player.Id,
                            LivingStatus = (int)LivingStatus.Alive,
                            ConfigId = _modelManager.Player.CharacterId,
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
                        PlayerId = _modelManager.Player.Id,
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
                                RoleId = _modelManager.Player.CharacterId,
                                RoleLevel = 1,
                            }
                        },
                        PlayerName = "ReversedRooms"
                    }
                },
                CurContextId = _modelManager.Player.Id
            }
        });

        await Session.PushMessage(MessageId.PushDataCompleteNotify, new PushDataCompleteNotify());

        await Session.Rpc.ReturnAsync(MessageId.EnterGameResponse, new EnterGameResponse());
    }

    [MessageHandler(MessageId.HeartbeatRequest)]
    public async Task OnHeartbeatRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.HeartbeatResponse, new HeartbeatResponse());
    }
}

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

        await Session.Rpc.ReturnAsync(MessageId.EnterGameResponse, new EnterGameResponse());
    }

    [MessageHandler(MessageId.EntityOnLandedRequest)]
    public async Task OnEntityOnLandedRequest(ReadOnlyMemory<byte> data)
    {
        await Session.Rpc.ReturnAsync(MessageId.EntityOnLandedResponse, new EntityOnLandedResponse
        {

        });
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

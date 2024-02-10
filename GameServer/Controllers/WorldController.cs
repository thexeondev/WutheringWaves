using GameServer.Controllers.Attributes;
using GameServer.Controllers.Event;
using GameServer.Models;
using GameServer.Network;
using GameServer.Network.Messages;
using Protocol;

namespace GameServer.Controllers;
internal class WorldController : Controller
{
    public WorldController(PlayerSession session) : base(session)
    {
        // WorldMessageHandler.
    }

    [GameEvent(GameEventType.EnterGame)]
    public async Task OnEnterGame(ModelManager modelManager)
    {
        PlayerModel player = modelManager.Player;

        await Session.Push(MessageId.JoinSceneNotify, new JoinSceneNotify
        {
            MaxEntityId = 2,
            TransitionOption = new TransitionOptionPb
            {
                TransitionType = (int)TransitionType.Empty
            },
            SceneInfo = new SceneInformation
            {
                OwnerId = player.Id,
                Mode = (int)SceneMode.Single,
                InstanceId = 8,
                AoiData = new PlayerSceneAoiData
                {
                    GenIds = { 1 },
                    Entities =
                    {
                        new EntityPb
                        {
                            EntityState = (int)EntityState.Born,
                            EntityType = (int)EEntityType.Player,
                            PlayerId = player.Id,
                            LivingStatus = (int)LivingStatus.Alive,
                            ConfigId = player.CharacterId,
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
                                                SkillId = 1001
                                            }
                                        }
                                    }
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
                        PlayerId = player.Id,
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
                                RoleId = player.CharacterId,
                                RoleLevel = 1,
                            }
                        },
                        PlayerName = player.Name
                    }
                },
                CurContextId = player.Id
            }
        });
    }

    [NetEvent(MessageId.EntityActiveRequest)]
    public ResponseMessage OnEntityActiveRequest() => Response(MessageId.EntityActiveResponse, new EntityActiveResponse());

    [NetEvent(MessageId.EntityOnLandedRequest)]
    public ResponseMessage OnEntityOnLandedRequest() => Response(MessageId.EntityOnLandedResponse, new EntityOnLandedResponse());

    [NetEvent(MessageId.PlayerMotionRequest)]
    public ResponseMessage OnPlayerMotionRequest() => Response(MessageId.PlayerMotionResponse, new PlayerMotionResponse());

    [NetEvent(MessageId.EntityLoadCompleteRequest)]
    public ResponseMessage OnEntityLoadCompleteRequest() => Response(MessageId.EntityLoadCompleteResponse, new EntityLoadCompleteResponse());

    [NetEvent(MessageId.SceneLoadingFinishRequest)]
    public ResponseMessage OnSceneLoadingFinishRequest() => Response(MessageId.SceneLoadingFinishResponse, new SceneLoadingFinishResponse());

    [NetEvent(MessageId.UpdateSceneDateRequest)]
    public ResponseMessage OnUpdateSceneDateRequest() => Response(MessageId.UpdateSceneDateResponse, new UpdateSceneDateResponse());
}

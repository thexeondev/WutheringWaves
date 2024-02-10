using GameServer.Controllers.Attributes;
using GameServer.Models;
using GameServer.Network;
using GameServer.Network.Messages;
using GameServer.Systems.Entity;
using GameServer.Systems.Entity.Component;
using GameServer.Systems.Event;
using Protocol;

namespace GameServer.Controllers;
internal class CreatureController : Controller
{
    private readonly EntitySystem _entitySystem;
    private readonly EntityFactory _entityFactory;
    private readonly ModelManager _modelManager;

    public CreatureController(PlayerSession session, EntitySystem entitySystem, EntityFactory entityFactory, ModelManager modelManager) : base(session)
    {
        _entitySystem = entitySystem;
        _entityFactory = entityFactory;
        _modelManager = modelManager;
    }

    public async Task JoinScene(int instanceId)
    {
        _modelManager.Creature.SetSceneLoadingData(instanceId);
        CreateInitialPlayer();

        await Session.Push(MessageId.JoinSceneNotify, new JoinSceneNotify
        {
            MaxEntityId = 10000,
            TransitionOption = new TransitionOptionPb
            {
                TransitionType = (int)TransitionType.Empty
            },
            SceneInfo = CreateSceneInfo()
        });
    }

    [NetEvent(MessageId.EntityActiveRequest)]
    public async Task<ResponseMessage> OnEntityActiveRequest(EntityActiveRequest request)
    {
        EntityActiveResponse response;

        EntityBase? entity = _entitySystem.Get<EntityBase>(request.EntityId);
        if (entity != null)
        {
            _entitySystem.Activate(entity);
            response = new EntityActiveResponse
            {
                ErrorCode = (int)ErrorCode.Success
            };

            response.ComponentPbs.AddRange(entity.ComponentSystem.Pb);
        }
        else
        {
            response = new EntityActiveResponse { ErrorCode = (int)ErrorCode.ErrActionEntityNoExist };
        }

        await OnVisionSkillChanged();
        return Response(MessageId.EntityActiveResponse, response);
    }

    [NetEvent(MessageId.SceneLoadingFinishRequest)]
    public ResponseMessage OnSceneLoadingFinishRequest()
    {
        _modelManager.Creature.OnWorldDone();

        return Response(MessageId.SceneLoadingFinishResponse, new SceneLoadingFinishResponse());
    }

    [GameEvent(GameEventType.VisionSkillChanged)]
    public async Task OnVisionSkillChanged()
    {
        PlayerEntity? playerEntity = GetPlayerEntity(_modelManager.Player.Id);
        if (playerEntity == null) return;

        EntityVisionSkillComponent visionSkillComponent = playerEntity.ComponentSystem.Get<EntityVisionSkillComponent>();

        VisionSkillChangeNotify skillChangeNotify = new() { EntityId = playerEntity.Id };
        skillChangeNotify.VisionSkillInfos.AddRange(visionSkillComponent.Skills);

        await Session.Push(MessageId.VisionSkillChangeNotify, skillChangeNotify);
    }

    public PlayerEntity? GetPlayerEntity(int playerId)
    {
        return _entitySystem.EnumerateEntities().FirstOrDefault(entity => entity is PlayerEntity p && p.PlayerId == playerId) as PlayerEntity;
    }

    private SceneInformation CreateSceneInfo()
    {
        SceneInformation scene = new()
        {
            InstanceId = _modelManager.Creature.InstanceId,
            OwnerId = _modelManager.Creature.OwnerId,
            CurContextId = _modelManager.Player.Id,
            TimeInfo = new(),
            AoiData = new(),
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
                    PlayerName = _modelManager.Player.Name
                }
            }
        };

        scene.AoiData.Entities.AddRange(_entitySystem.Pb);
        return scene;
    }

    private void CreateInitialPlayer()
    {
        PlayerEntity entity = _entityFactory.CreatePlayer(1601, _modelManager.Player.Id);
        entity.Pos = new()
        {
            X = 4000,
            Y = -2000,
            Z = 260
        };

        _entitySystem.Create(entity);
    }
}

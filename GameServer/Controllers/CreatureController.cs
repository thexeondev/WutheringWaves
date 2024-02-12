using Core.Config;
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
    private readonly ConfigManager _configManager;

    public CreatureController(PlayerSession session, EntitySystem entitySystem, EntityFactory entityFactory, ModelManager modelManager, ConfigManager configManager) : base(session)
    {
        _entitySystem = entitySystem;
        _entityFactory = entityFactory;
        _modelManager = modelManager;
        _configManager = configManager;
    }

    public async Task JoinScene(int instanceId)
    {
        _modelManager.Creature.SetSceneLoadingData(instanceId);
        CreateTeamPlayerEntities();

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

    [GameEvent(GameEventType.FormationUpdated)]
    public async Task OnFormationUpdated()
    {
        // Remove old entities

        IEnumerable<PlayerEntity> oldEntities = _entitySystem.EnumerateEntities()
                                                   .Where(e => e is PlayerEntity entity && entity.PlayerId == _modelManager.Player.Id)
                                                   .Cast<PlayerEntity>().ToArray();

        foreach (PlayerEntity oldEntity in oldEntities)
        {
            _entitySystem.Destroy(oldEntity);
        }

        await Session.Push(MessageId.EntityRemoveNotify, new EntityRemoveNotify
        {
            IsRemove = true,
            RemoveInfos =
            {
                oldEntities.Select(entity => new EntityRemoveInfo
                {
                    EntityId = entity.Id,
                    Type = (int)entity.Type
                })
            }
        });

        // Spawn new entities

        CreateTeamPlayerEntities();

        IEnumerable<PlayerEntity> newEntities = _entitySystem.EnumerateEntities()
                                                   .Where(e => e is PlayerEntity entity && entity.PlayerId == _modelManager.Player.Id)
                                                   .Cast<PlayerEntity>();

        await Session.Push(MessageId.EntityAddNotify, new EntityAddNotify
        {
            IsAdd = true,
            EntityPbs =
            {
                newEntities.Select(entity => entity.Pb)
            }
        });

        _modelManager.Creature.PlayerEntityId = newEntities.First().Id;
        await Session.Push(MessageId.UpdatePlayerAllFightRoleNotify, new UpdatePlayerAllFightRoleNotify
        {
            PlayerId = _modelManager.Player.Id,
            FightRoleInfos = { GetFightRoleInfos() }
        });
    }

    [GameEvent(GameEventType.VisionSkillChanged)]
    public async Task OnVisionSkillChanged()
    {
        PlayerEntity? playerEntity = GetPlayerEntity();
        if (playerEntity == null) return;

        EntityVisionSkillComponent visionSkillComponent = playerEntity.ComponentSystem.Get<EntityVisionSkillComponent>();

        VisionSkillChangeNotify skillChangeNotify = new() { EntityId = playerEntity.Id };
        skillChangeNotify.VisionSkillInfos.AddRange(visionSkillComponent.Skills);

        await Session.Push(MessageId.VisionSkillChangeNotify, skillChangeNotify);
    }

    public PlayerEntity? GetPlayerEntity()
    {
        return _entitySystem.Get<PlayerEntity>(_modelManager.Creature.PlayerEntityId);
    }

    public PlayerEntity? GetPlayerEntityByRoleId(int roleId)
    {
        return _entitySystem.EnumerateEntities()
        .FirstOrDefault(e => e is PlayerEntity playerEntity && playerEntity.ConfigId == roleId && playerEntity.PlayerId == _modelManager.Player.Id) as PlayerEntity;
    }

    public async Task SwitchPlayerEntity(int roleId)
    {
        PlayerEntity? prevEntity = GetPlayerEntity();
        if (prevEntity == null) return;

        prevEntity.IsCurrentRole = false;

        PlayerEntity? newEntity = _entitySystem.EnumerateEntities().FirstOrDefault(e => e is PlayerEntity playerEntity && playerEntity.ConfigId == roleId) as PlayerEntity;
        if (newEntity == null) return;

        _modelManager.Creature.PlayerEntityId = newEntity.Id;
        newEntity.IsCurrentRole = true;

        await OnVisionSkillChanged();
    }

    private SceneInformation CreateSceneInfo() => new()
    {
        InstanceId = _modelManager.Creature.InstanceId,
        OwnerId = _modelManager.Creature.OwnerId,
        CurContextId = _modelManager.Player.Id,
        TimeInfo = new(),
        AoiData = new PlayerSceneAoiData
        {
            Entities = { _entitySystem.Pb }
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
                PlayerName = _modelManager.Player.Name,
                FightRoleInfos = { GetFightRoleInfos() }
            }
        }
    };

    private IEnumerable<FightRoleInformation> GetFightRoleInfos()
    {
        IEnumerable<PlayerEntity> playerEntities = _entitySystem.EnumerateEntities()
                                                   .Where(e => e is PlayerEntity entity && entity.PlayerId == _modelManager.Player.Id)
                                                   .Cast<PlayerEntity>();

        return playerEntities.Select(playerEntity => new FightRoleInformation
        {
            EntityId = playerEntity.Id,
            CurHp = playerEntity.Health,
            MaxHp = playerEntity.HealthMax,
            IsControl = playerEntity.Id == _modelManager.Creature.PlayerEntityId,
            RoleId = playerEntity.ConfigId,
            RoleLevel = 1,
        });
    }

    private void CreateTeamPlayerEntities()
    {
        for (int i = 0; i < _modelManager.Formation.RoleIds.Length; i++)
        {
            PlayerEntity entity = _entityFactory.CreatePlayer(_modelManager.Formation.RoleIds[i], _modelManager.Player.Id);
            entity.Pos = new()
            {
                X = 4000,
                Y = -2000,
                Z = 260
            };
            entity.IsCurrentRole = i == 0;

            _entitySystem.Create(entity);

            // Give weapon to entity
            RoleInfoConfig roleConfig = _configManager.GetConfig<RoleInfoConfig>(entity.ConfigId)!;
            WeaponConfig weaponConfig = _configManager.GetConfig<WeaponConfig>(roleConfig.InitWeaponItemId)!;
            entity.WeaponId = weaponConfig.ItemId;

            if (i == 0) _modelManager.Creature.PlayerEntityId = entity.Id;
        }
    }
}

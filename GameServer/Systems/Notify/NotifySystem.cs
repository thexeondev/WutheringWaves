using System.Data;
using GameServer.Models;
using GameServer.Network;
using GameServer.Systems.Entity;
using Protocol;

namespace GameServer.Systems.Notify;
internal class NotifySystem : IGameActionListener
{
    private readonly PlayerSession _session;
    private readonly ModelManager _modelManager;

    public NotifySystem(PlayerSession session, ModelManager modelManager)
    {
        _session = session;
        _modelManager = modelManager;
    }

    public Task OnJoinedScene(SceneInformation sceneInformation, TransitionType transitionType)
    {
        return _session.Push(MessageId.JoinSceneNotify, new JoinSceneNotify
        {
            SceneInfo = sceneInformation,
            TransitionOption = new TransitionOptionPb
            {
                TransitionType = (int)transitionType
            }
        });
    }

    public Task OnEntitiesAdded(IEnumerable<EntityBase> entities)
    {
        if (_modelManager.Creature.LoadingWorld) return Task.CompletedTask;

        return _session.Push(MessageId.EntityAddNotify, new EntityAddNotify
        {
            IsAdd = true,
            EntityPbs = { entities.Select(e => e.Pb) }
        });
    }

    public Task OnEntitiesRemoved(IEnumerable<EntityBase> entities)
    {
        if (_modelManager.Creature.LoadingWorld) return Task.CompletedTask;

        return _session.Push(MessageId.EntityRemoveNotify, new EntityRemoveNotify
        {
            IsRemove = true,
            RemoveInfos =
            {
                entities.Select(e => new EntityRemoveInfo
                {
                    EntityId = e.Id,
                    Type = (int)ERemoveEntityType.RemoveTypeNormal
                })
            }
        });
    }

    public Task OnPlayerFightRoleInfoUpdated(int playerId, IEnumerable<FightRoleInformation> fightRoles)
    {
        return _session.Push(MessageId.UpdatePlayerAllFightRoleNotify, new UpdatePlayerAllFightRoleNotify
        {
            PlayerId = playerId,
            FightRoleInfos = { fightRoles }
        });
    }

    public Task OnRolePropertiesUpdated(int roleId, IEnumerable<ArrayIntInt> baseProp, IEnumerable<ArrayIntInt> addProp)
    {
        if (_modelManager.Creature.LoadingWorld) return Task.CompletedTask;

        return _session.Push(MessageId.PbRolePropsNotify, new PbRolePropsNotify
        {
            RoleId = roleId,
            BaseProp = { baseProp },
            AddProp = { addProp }
        });
    }

    public Task OnEntityEquipmentChanged(long entityId, EquipComponentPb componentPb)
    {
        return _session.Push(MessageId.EntityEquipChangeNotify, new EntityEquipChangeNotify
        {
            EntityId = entityId,
            EquipComponent = componentPb
        });
    }

    public Task OnEntityAttributesChanged(long entityId, IEnumerable<GameplayAttributeData> attributes)
    {
        return _session.Push(MessageId.AttributeChangedNotify, new AttributeChangedNotify
        {
            Id = entityId,
            Attributes = { attributes }
        });
    }
}

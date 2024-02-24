using GameServer.Systems.Entity;
using Protocol;

namespace GameServer.Systems.Notify;
internal interface IGameActionListener
{
    Task OnJoinedScene(SceneInformation sceneInformation, TransitionType transitionType);
    Task OnEntitiesAdded(IEnumerable<EntityBase> entities);
    Task OnEntitiesRemoved(IEnumerable<EntityBase> entities);
    Task OnPlayerFightRoleInfoUpdated(int playerId, IEnumerable<FightRoleInformation> fightRoles);
    Task OnRolePropertiesUpdated(int roleId, IEnumerable<ArrayIntInt> baseProp, IEnumerable<ArrayIntInt> addProp);
    Task OnEntityEquipmentChanged(long entityId, EquipComponentPb componentPb);
    Task OnEntityAttributesChanged(long entityId, IEnumerable<GameplayAttributeData> attributes);
}

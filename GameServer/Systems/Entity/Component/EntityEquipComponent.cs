using Protocol;

namespace GameServer.Systems.Entity.Component;
internal class EntityEquipComponent : EntityComponentBase
{
    public int WeaponId { get; set; }

    public EntityEquipComponent()
    {
        // EntityEquipComponent.
    }

    public override EntityComponentType Type => EntityComponentType.Equip;

    public override EntityComponentPb Pb => new()
    {
        EquipComponent = new EquipComponentPb
        {
            WeaponId = WeaponId
        }
    };
}

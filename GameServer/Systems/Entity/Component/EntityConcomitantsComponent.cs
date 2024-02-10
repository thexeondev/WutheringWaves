using Protocol;

namespace GameServer.Systems.Entity.Component;
internal class EntityConcomitantsComponent : EntityComponentBase
{
    public List<long> CustomEntityIds { get; }

    public EntityConcomitantsComponent()
    {
        CustomEntityIds = [];
    }

    public override EntityComponentType Type => EntityComponentType.Concomitants;

    public override EntityComponentPb Pb
    {
        get
        {
            EntityComponentPb pb = new()
            {
                ConcomitantsComponentPb = new()
            };

            pb.ConcomitantsComponentPb.CustomEntityIds.AddRange(CustomEntityIds);
            return pb;
        }
    }
}

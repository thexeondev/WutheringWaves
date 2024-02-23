using Protocol;

namespace GameServer.Systems.Entity.Component;
internal class EntityConcomitantsComponent : EntityComponentBase
{
    public List<long> CustomEntityIds { get; }
    public long PhantomRoleEntityId { get; set; }
    public long VisionEntityId { get; set; }

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
                {
                    PhantomRoleEid = PhantomRoleEntityId,
                    VisionEntityId = VisionEntityId
                }
            };

            pb.ConcomitantsComponentPb.CustomEntityIds.AddRange(CustomEntityIds);
            return pb;
        }
    }
}

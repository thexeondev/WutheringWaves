using Protocol;

namespace GameServer.Systems.Entity.Component;
internal class EntityFsmComponent : EntityComponentBase
{
    public List<DFsm> Fsms { get; } = [];

    public override EntityComponentType Type => EntityComponentType.EntityFsm;

    public override EntityComponentPb Pb => new()
    {
        EntityFsmComponentPb = new()
        {
            Fsms =
            {
                Fsms
            }
        },
    };
}

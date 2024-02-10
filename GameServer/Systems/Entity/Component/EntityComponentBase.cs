using Protocol;

namespace GameServer.Systems.Entity.Component;
internal abstract class EntityComponentBase
{
    public abstract EntityComponentType Type { get; }
    public abstract EntityComponentPb Pb { get; }
}

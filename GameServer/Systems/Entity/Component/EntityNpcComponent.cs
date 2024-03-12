using Protocol;

namespace GameServer.Systems.Entity.Component;
internal class EntityNpcComponent : EntityComponentBase
{
    public override EntityComponentType Type => EntityComponentType.Npc;
 
    public long PasserbyNpcOwnerId { get; set; }
    public int SplineIndex { get; set; }


    public override EntityComponentPb Pb => new()
    {
        NpcPb = new NpcPb
        {
            PasserbyNpcOwnerId = PasserbyNpcOwnerId,
            SplineIndex = SplineIndex
        }
    };
}

using Protocol;

namespace GameServer.Systems.Entity.Component;
internal class EntityLogicStateComponent : EntityComponentBase
{
    public List<int> States { get; set; }

    public EntityLogicStateComponent()
    {
        States = [];
    }

    public override EntityComponentType Type => EntityComponentType.LogicState;

    public override EntityComponentPb Pb => new()
    {
        LogicStateComponentPb = new()
        {
            States = { States }
        }
    };
}

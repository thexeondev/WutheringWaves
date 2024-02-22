using Protocol;

namespace GameServer.Systems.Entity.Component;
internal class EntityFightBuffComponent : EntityComponentBase
{
    public List<FightBuffInformation> BuffInfoList { get; } = [];

    public override EntityComponentType Type => EntityComponentType.FightBuff;

    public override EntityComponentPb Pb => new()
    {
        FightBuffComponent = new()
        {
            FightBuffInfos =
            {
                BuffInfoList
            }
        }
    };
}

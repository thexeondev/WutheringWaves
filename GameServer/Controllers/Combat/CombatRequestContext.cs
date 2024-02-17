using Protocol;

namespace GameServer.Controllers.Combat;
internal class CombatRequestContext
{
    public CombatRequestData Request { get; }
    public List<CombatNotifyData> Notifies { get; }

    public CombatRequestContext(CombatRequestData request)
    {
        Request = request;
        Notifies = [];
    }
}

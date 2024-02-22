using Protocol;

namespace GameServer.Controllers.Attributes;

[AttributeUsage(AttributeTargets.Method)]
internal class CombatRequestAttribute : Attribute
{
    public CombatRequestData.MessageOneofCase MessageCase { get; }

    public CombatRequestAttribute(CombatRequestData.MessageOneofCase messageCase)
    {
        MessageCase = messageCase;
    }
}

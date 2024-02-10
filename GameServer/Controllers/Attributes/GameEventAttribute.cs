using GameServer.Systems.Event;

namespace GameServer.Controllers.Attributes;

[AttributeUsage(AttributeTargets.Method)]
internal class GameEventAttribute : Attribute
{
    public GameEventType Type { get; }

    public GameEventAttribute(GameEventType type)
    {
        Type = type;
    }
}

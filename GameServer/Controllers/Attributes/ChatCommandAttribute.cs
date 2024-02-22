namespace GameServer.Controllers.Attributes;

[AttributeUsage(AttributeTargets.Method)]
internal class ChatCommandAttribute : Attribute
{
    public string Command { get; }

    public ChatCommandAttribute(string command)
    {
        Command = command;
    }
}

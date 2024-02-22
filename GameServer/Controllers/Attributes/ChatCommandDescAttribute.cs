namespace GameServer.Controllers.Attributes;

[AttributeUsage(AttributeTargets.Method)]
internal class ChatCommandDescAttribute : Attribute
{
    public string Description { get; }

    public ChatCommandDescAttribute(string description)
    {
        Description = description;
    }
}

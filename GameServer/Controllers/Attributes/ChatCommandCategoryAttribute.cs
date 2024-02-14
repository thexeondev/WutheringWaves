namespace GameServer.Controllers.Attributes;

[AttributeUsage(AttributeTargets.Class)]
internal class ChatCommandCategoryAttribute : Attribute
{
    public string Category { get; }

    public ChatCommandCategoryAttribute(string category)
    {
        Category = category;
    }
}

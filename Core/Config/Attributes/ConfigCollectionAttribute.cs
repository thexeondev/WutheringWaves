namespace Core.Config.Attributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
internal class ConfigCollectionAttribute : Attribute
{
    public string Path { get; }

    public ConfigCollectionAttribute(string path)
    {
        Path = path;
    }
}

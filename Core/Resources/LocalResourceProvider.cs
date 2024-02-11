using System.Text.Json;

namespace Core.Resources;
internal class LocalResourceProvider : IResourceProvider
{
    public JsonDocument GetJsonResource(string path)
    {
        using FileStream fileStream = File.Open(path, FileMode.Open, FileAccess.Read);
        return JsonDocument.Parse(fileStream);
    }
}

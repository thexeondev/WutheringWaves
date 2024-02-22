using System.Text.Json;

namespace Core.Resources;
public interface IResourceProvider
{
    JsonDocument GetJsonResource(string path);
}

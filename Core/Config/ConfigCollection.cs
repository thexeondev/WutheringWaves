using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace Core.Config;
public class ConfigCollection
{
    private readonly ImmutableDictionary<int, IConfig> _configs;

    public ConfigCollection(JsonDocument json, Type type)
    {
        _configs = LoadConfigs(json, type);
    }

    public int Count => _configs.Count;

    public TConfig At<TConfig>(int index) where TConfig : IConfig
    {
        return (TConfig)_configs.Values.ElementAt(index);
    }

    public IEnumerable<TConfig> Enumerate<TConfig>() where TConfig : IConfig
    {
        return _configs.Values.Cast<TConfig>();
    }

    public bool TryGet<TConfig>(int identifier, [NotNullWhen(true)] out TConfig? config) where TConfig : IConfig
    {
        bool result = _configs.TryGetValue(identifier, out IConfig? cfg);

        config = (TConfig?)cfg;
        return result;
    }

    private static ImmutableDictionary<int, IConfig> LoadConfigs(JsonDocument json, Type type)
    {
        var builder = ImmutableDictionary.CreateBuilder<int, IConfig>();

        foreach (JsonElement element in json.RootElement.EnumerateArray())
        {
            if (element.ValueKind != JsonValueKind.Object) throw new InvalidDataException($"LoadConfigs: expected array of {JsonValueKind.Object}, got array of {element.ValueKind}");

            IConfig configItem = (element.Deserialize(type) as IConfig)!;
            builder.Add(configItem.Identifier, configItem);
        }

        return builder.ToImmutable();
    }
}

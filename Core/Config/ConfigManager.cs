using System.Collections.Immutable;
using System.Reflection;
using Core.Config.Attributes;
using Core.Resources;
using Microsoft.Extensions.Logging;

namespace Core.Config;
public class ConfigManager
{
    private readonly ImmutableDictionary<ConfigType, ConfigCollection> _collectionsByEnum;
    private readonly ImmutableDictionary<Type, ConfigCollection> _collectionsByType;

    public ConfigManager(ILogger<ConfigManager> logger, IResourceProvider resourceProvider)
    {
        (_collectionsByEnum, _collectionsByType) = LoadConfigCollections(resourceProvider);
        logger.LogInformation("Loaded {count} config collections", _collectionsByEnum.Count);
    }

    public IEnumerable<TConfig> Enumerate<TConfig>() where TConfig : IConfig
    {
        return GetCollection<TConfig>().Enumerate<TConfig>();
    }

    public ConfigCollection GetCollection<TConfigType>() where TConfigType : IConfig
    {
        return _collectionsByType[typeof(TConfigType)];
    }

    public ConfigCollection GetCollection(ConfigType type)
    {
        return _collectionsByEnum[type];
    }

    public TConfig? GetConfig<TConfig>(int id) where TConfig : IConfig
    {
        if (_collectionsByType[typeof(TConfig)].TryGet(id, out TConfig? config))
            return config;

        return default;
    }

    private static (ImmutableDictionary<ConfigType, ConfigCollection>, ImmutableDictionary<Type, ConfigCollection>) LoadConfigCollections(IResourceProvider resourceProvider)
    {
        var builderByEnum = ImmutableDictionary.CreateBuilder<ConfigType, ConfigCollection>();
        var builderByType = ImmutableDictionary.CreateBuilder<Type, ConfigCollection>();

        IEnumerable<Type> types = Assembly.GetExecutingAssembly().GetTypes()
                                  .Where(type => type.IsAssignableTo(typeof(IConfig)) && !type.IsAbstract);

        foreach (Type type in types)
        {
            ConfigCollectionAttribute? attribute = type.GetCustomAttribute<ConfigCollectionAttribute>();
            if (attribute == null) continue;

            ConfigCollection collection = new(resourceProvider.GetJsonResource("data/config/" + attribute.Path), type);
            builderByEnum.Add(collection.At<IConfig>(0).Type, collection);
            builderByType.Add(collection.At<IConfig>(0).GetType(), collection);
        }
        
        return (builderByEnum.ToImmutable(), builderByType.ToImmutable());
    }
}

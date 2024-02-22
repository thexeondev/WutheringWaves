namespace Core.Config;
public interface IConfig
{
    ConfigType Type { get; }
    int Identifier { get; }
}

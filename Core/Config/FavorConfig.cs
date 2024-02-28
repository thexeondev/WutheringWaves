using Core.Config.Attributes;

namespace Core.Config;

[ConfigCollection("role/favorword.json")]
public class FavorWordConfig : IConfig
{
    public ConfigType Type => ConfigType.FavorWord;
    public int Identifier => Id;

    public int Id { get; set; }
    public int RoleId { get; set; }
    public int CondGroupId { get; set; }

}
[ConfigCollection("role/favorgoods.json")]
public class FavorGoodsConfig : IConfig
{
    public ConfigType Type => ConfigType.FavorGoods;
    public int Identifier => Id;

    public int Id { get; set; }
    public int RoleId { get; set; }
    public int CondGroupId { get; set; }

}
[ConfigCollection("role/favorstory.json")]
public class FavorStoryConfig : IConfig
{
    public ConfigType Type => ConfigType.FavorStory;
    public int Identifier => Id;

    public int Id { get; set; }
    public int RoleId { get; set; }
    public int CondGroupId { get; set; }

}


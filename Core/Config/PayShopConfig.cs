using Core.Config.Attributes;

namespace Core.Config;

[ConfigCollection("payshop/payshop.json")]
public class PayShopConfig : IConfig
{
    public ConfigType Type => ConfigType.Payshop;
    public int Identifier => Id;
    public int Id { get; set; }
    public int DynamicTabId { get; set; }
    public List<int> Money { get; set; } = [];
    public bool Enable { get; set; }
}

[ConfigCollection("payshop/payshopgoods.json")]
public class PayshopgoodConfig : IConfig
{
    public ConfigType Type => ConfigType.Payshopgood;
    public int Identifier => Id;
    public int Id { get; set; }
    public int ShopId { get; set; }
    public int TabId { get; set; }
    public int ItemId { get; set; }
    public int Sort { get; set; }
    public int ItemCount { get; set; }
    public int ConditionId { get; set; }
    public int BuyConditionId { get; set; }
    public int BuyLimit { get; set; }
    //public required string SellTimeText { get; set; }
    public int MoneyId { get; set; }
    public int Price { get; set; }
    public int PromotionPrice { get; set; }
    public int PromotionShow { get; set; }
    //public required string PromotionTimeText { get; set; }
    //public required string Banner { get; set; }
    public bool Enable { get; set; }
    public bool Show { get; set; }

}



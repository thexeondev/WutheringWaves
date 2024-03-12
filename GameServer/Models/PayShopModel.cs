using Core.Config;
using Google.Protobuf.Collections;
using Protocol;
using System.Diagnostics;
using System.Net.Sockets;

namespace GameServer.Models;

internal class PayShopModel
{
    public PayShopInfo Payshop1List { get; set; } = new();
    public PayShopInfo Payshop2List { get; set; } = new();
    public PayShopInfo Payshop3List { get; set; } = new();
    public PayShopInfo Payshop4List { get; set; } = new();
    public PayShopInfo Payshop5List { get; set; } = new();
    public PayShopInfo Payshop100List { get; set; } = new();
    public PayShopInfo Payshop201List { get; set; } = new();

    //1,2,3,4,5,100,201
    public List<PayShopItem> Payshopgood1List { get; } = [];
    public List<PayShopItem> Payshopgood2List { get; } = [];
    public List<PayShopItem> Payshopgood3List { get; } = [];
    public List<PayShopItem> Payshopgood4List { get; } = [];
    public List<PayShopItem> Payshopgood5List { get; } = [];
    public List<PayShopItem> Payshopgood100List { get; } = [];
    public List<PayShopItem> Payshopgood201List { get; } = [];

    public void ClearPayShop()
    {
        Payshop1List = new();
        Payshop2List = new();
        Payshop3List = new();
        Payshop4List = new();
        Payshop5List = new();
        Payshop100List = new();
        Payshop201List = new();
        Payshopgood1List.Clear();
        Payshopgood2List.Clear();
        Payshopgood3List.Clear();
        Payshopgood4List.Clear();
        Payshopgood5List.Clear();
        Payshopgood100List.Clear();
        Payshopgood201List.Clear();

    }
    public void AddPayShop(int shopId, long UpdateTime)
    {
        switch (shopId)
        {
            case 1:
                Payshop1List.Id = 1;
                Payshop1List.UpdateTime = UpdateTime;
                Payshop1List.Items.Add(Payshopgood1List);
                break;
            case 2:
                Payshop2List.Id = 2;
                Payshop2List.UpdateTime = UpdateTime;
                Payshop2List.Items.Add(Payshopgood2List);
                break;
            case 3:
                Payshop3List.Id = 3;
                Payshop3List.UpdateTime = UpdateTime;
                Payshop3List.Items.Add(Payshopgood3List);
                break;
            case 4:
                Payshop4List.Id = 4;
                Payshop4List.UpdateTime = UpdateTime;
                Payshop4List.Items.Add(Payshopgood4List);
                break;
            case 5:
                Payshop5List.Id = 5;
                Payshop5List.UpdateTime = UpdateTime;
                Payshop5List.Items.Add(Payshopgood5List);
                break;
            case 100:
                Payshop100List.Id = 100;
                Payshop100List.UpdateTime = UpdateTime;
                Payshop100List.Items.Add(Payshopgood100List);
                break;
            case 201:
                Payshop201List.Id =201;
                Payshop201List.UpdateTime = UpdateTime;
                Payshop201List.Items.Add(Payshopgood201List);
                break;
            default:
                break;
        }
    }
    public PayShopItem AddPayShopgood(int shopId, PayshopgoodConfig config,  int boughtcount, PayShopUpdateType updatetype, PayShopItemType Itemtype)
    {
        PayShopItem items = new()
        {
            Id = config.Id,
            TabId = config.TabId,
            ItemId = config.ItemId,
            ItemCount = config.ItemCount,
            Locked = false,//config.Enable,
            BuyLimit = config.BuyLimit,
            BoughtCount = boughtcount,
            Price = new PayShopPrice
            {
                Id = config.MoneyId,
                Count = config.Price,
                PromotionCount = config.PromotionPrice
            },
            BeginTime = 0,
            EndTime = Int32.MaxValue,
            BeginPromotionTime = 0,
            EndPromotionTime = Int32.MaxValue,
            UpdateType = (int)updatetype,
            UpdateTime = 0,
            ShopItemType = (int)Itemtype,
            Tag = 1,
            TagBeginTime = 0,
            TagEndTime = Int32.MaxValue,
            Sort = config.Sort,
            GoodsTitle = {  },
            PromotionShow = config.PromotionShow,
            CanBuyGoods = true//config.Enable
        };
        switch (shopId)
        {
            case 1:
                Payshopgood1List.Add(items);
                break;
            case 2:
                Payshopgood2List.Add(items);
                break;
            case 3:
                Payshopgood3List.Add(items);
                break;
            case 4:
                Payshopgood4List.Add(items);
                break;
            case 5:
                Payshopgood5List.Add(items);
                break;
            case 100:
                Payshopgood100List.Add(items);
                break;
            case 201:
                Payshopgood201List.Add(items);
                break;
            default:
                break;
        }
        return items;
    }



}








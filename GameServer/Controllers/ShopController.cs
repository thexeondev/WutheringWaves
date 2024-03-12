using Core.Config;
using GameServer.Controllers.Attributes;
using GameServer.Extensions.Logic;
using GameServer.Models;
using GameServer.Network;
using GameServer.Systems.Event;
using GameServer.Systems.Notify;
using Protocol;

namespace GameServer.Controllers;
internal class ShopController : Controller
{
    private readonly ModelManager _modelManager;
    private readonly ConfigManager _configManager;
    public ShopController(PlayerSession session, ConfigManager configManager, ModelManager modelManager) : base(session)
    {
        _modelManager = modelManager;
        _configManager = configManager;
        // ShopController.
        //PayShop
    }

    [NetEvent(MessageId.PayShopInfoRequest)]
    public RpcResult OnPayShopInfoRequest(/*PayShopInfoRequest request*/)
    {
        return Response(MessageId.PayShopInfoResponse, new PayShopInfoResponse
        {
            Version = "Reversed Room",
            ErrorCode = (int)ErrorCode.Success,
            Infos = {
                _modelManager.PayShop.Payshop1List,
                _modelManager.PayShop.Payshop2List,
                _modelManager.PayShop.Payshop3List,
                _modelManager.PayShop.Payshop4List,
                _modelManager.PayShop.Payshop5List,
                _modelManager.PayShop.Payshop100List,
                _modelManager.PayShop.Payshop201List
            }

        });
    }

    [GameEvent(GameEventType.EnterGame)]
    public async Task OnPayShopUnlockNotify()
    {
        PayShopUnlockNotify notify = new()
        {
            UnlockList = { 1, 2, 3, 4, 5, 100, 201 }
        };
        await Session.Push(MessageId.PayShopUnlockNotify, notify);
    }

    [GameEvent(GameEventType.EnterGame)]
    public async Task OnPayShopInfoNotify()
    {
        _modelManager.PayShop.ClearPayShop();
        foreach (PayShopConfig payshopconfig in _configManager.Enumerate<PayShopConfig>())
        {
            //if (payshopconfig.Enable)
           // {
                foreach (PayshopgoodConfig payshopgoodconfig in _configManager.Enumerate<PayshopgoodConfig>())
                {
                    if (payshopconfig.Id == payshopgoodconfig.ShopId)
                    {
                        _modelManager.PayShop.AddPayShopgood(payshopconfig.Id, payshopgoodconfig,  1, PayShopUpdateType.Forever, PayShopItemType.Normal);
                    }
                }
                _modelManager.PayShop.AddPayShop(payshopconfig.Id, 0);
            //}
        }
        await Session.Push(MessageId.PayShopInfoNotify, new PayShopInfoNotify
        {
            Version = "Reversed Room",
            Infos = { 
                _modelManager.PayShop.Payshop1List,
                _modelManager.PayShop.Payshop2List,
                _modelManager.PayShop.Payshop3List,
                _modelManager.PayShop.Payshop4List,
                _modelManager.PayShop.Payshop5List,
                _modelManager.PayShop.Payshop100List,
                _modelManager.PayShop.Payshop201List
            }
        }
);
    }

    [NetEvent(MessageId.PayItemRequest)]
    public RpcResult OnPayItemRequest()
    {
        //id_ = other.id_;
        //version_ = other.version_;

        return Response(MessageId.PayItemResponse, new PayItemResponse
        {
            ReceiptId = "Reversed Room",
            ErrorCode = (int)ErrorCode.Success
        });
    }//wip

    [NetEvent(MessageId.PayInfoRequest)]
    public RpcResult OnPayInfoRequest()
    {
        return Response(MessageId.PayInfoResponse, new PayInfoResponse
        {
            Infos = { new PayItemInfo { } },
            Version = "Reversed Room",
            ErrorCode = (int)ErrorCode.Success
        });
    }//wip

    [NetEvent(MessageId.PayShopBuyRequest)]
    public RpcResult OnPayShopBuyRequest() => Response(MessageId.PayShopBuyResponse, new PayShopBuyResponse());

    [NetEvent(MessageId.PayShopDirectBuyRequest)]
    public RpcResult OnPayShopDirectBuyRequest() => Response(MessageId.PayShopDirectBuyResponse, new PayShopDirectBuyResponse());

    [NetEvent(MessageId.PayShopItemUpdateRequest)]
    public RpcResult OnPayShopItemUpdateRequest(/*PayShopItemUpdateRequest request*/)
    {
        //request.ShopItemIds
        return Response(MessageId.PayShopItemUpdateResponse, new PayShopItemUpdateResponse
        {
            ErrorCode = (int)ErrorCode.Success,
            Items = {
            _modelManager.PayShop.Payshopgood1List,
            _modelManager.PayShop.Payshopgood2List,
            _modelManager.PayShop.Payshopgood3List,
            _modelManager.PayShop.Payshopgood4List,
            _modelManager.PayShop.Payshopgood5List,
            _modelManager.PayShop.Payshopgood100List,
            _modelManager.PayShop.Payshopgood201List,
            }
        });
    }
    [NetEvent(MessageId.PayShopUpdateRequest)]
    public RpcResult OnPayShopUpdateRequest(PayShopUpdateRequest request) //=> Response(MessageId.PayShopUpdateResponse, new PayShopUpdateResponse());
    {
        PayShopUpdateResponse response = new()
        {
            ErrorCode = (int)ErrorCode.Success
        };
        int id = request.Id;
        switch(id)
        {
            case 1:
                response.Info = _modelManager.PayShop.Payshop1List;
                break;
            case 2:
                response.Info = _modelManager.PayShop.Payshop2List;
                break;
            case 3:
                response.Info = _modelManager.PayShop.Payshop3List;
                break;
            case 4:
                response.Info = _modelManager.PayShop.Payshop4List;
                break;
            case 5:
                response.Info = _modelManager.PayShop.Payshop5List;
                break;
            case 100:
                response.Info = _modelManager.PayShop.Payshop100List;
                break;
             case 201:
                response.Info = _modelManager.PayShop.Payshop201List;
                break;
            default:
                response.ErrorCode = (int)ErrorCode.ErrPayShopNotExists;
                break;
        }
            return Response(MessageId.PayShopUpdateResponse, response);
    }
    [NetEvent(MessageId.ShopBuyRequest)]
    public RpcResult OnShopBuyRequest() => Response(MessageId.ShopBuyResponse, new ShopBuyResponse());

    [NetEvent(MessageId.ShopInfoRequest)]
    public RpcResult OnShopInfoRequest() => Response(MessageId.ShopInfoResponse, new ShopInfoResponse());

    [NetEvent(MessageId.ShopUpdateRequest)]
    public RpcResult OnShopUpdateRequest() => Response(MessageId.ShopUpdateResponse, new ShopUpdateResponse());

    //[GameEvent(GameEventType.EnterGame)]
    //public async Task OnShopInfoNotify()
    //{

    //    ShopInfoNotify notify = new();
    //    {
    //        notify.VersionStr = "Reversed Room";
    //        notify.ShopList.Add(new ShopInfo            
    //        {
    //            ShopId = 3,
    //            UpdateTime = 0,
    //            ItemInfoList =
    //            {
    //                new  ShopItemInfoNew
    //                {
    //                    Id = 42,
    //                    BoughtCount  = 2,
    //                    Lock = false,
    //                    ItemId = 50011,
    //                    ItemNum = 10,
    //                    CondText ="",
    //                    MoneyList = {
    //                     new ShoppMoneyInfo
    //                    {
    //                        MoneyId =4,
    //                        MoneyNum =30
    //                    }},
    //                    BeginTime = 0,
    //                    EndTime = Int32.MaxValue,
    //                    LimitNum = 10,
    //                    OriginalMoneyList = {
    //                    new ShoppMoneyInfo
    //                    { 
    //                        MoneyId =4,
    //                        MoneyNum =30
    //                    }},
    //                    Label ="Reversed Room",
    //                    SwitchText ="Reversed Room",
    //                    PurchaseText ="Reversed Room -100%"
    //                 }

    //            }
    //        });

    //    }
    //    await Session.Push(MessageId.ShopInfoNotify, notify);
    //}
    //[GameEvent(GameEventType.EnterGame)]
    public async Task OnShopUnlockNotify()
    {

        ShopUnlockNotify notify = new();
        {
            notify.UnlockList.Add(new UnlockInfo
            {
                ShopId = 1,
                Id = 1,
            });
        }
        await Session.Push(MessageId.ShopUnlockNotify, notify);
    }
}

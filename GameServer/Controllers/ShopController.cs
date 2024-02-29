using GameServer.Controllers.Attributes;
using GameServer.Network;
using Google.Protobuf.Collections;
using Protocol;

namespace GameServer.Controllers;
internal class ShopController : Controller
{
    public ShopController(PlayerSession session) : base(session)
    {
        // ShopController.
        //PayShop
    }

    [NetEvent(MessageId.PayShopInfoRequest)]
    public RpcResult OnPayShopInfoRequest(PayShopInfoRequest request)
    {
        var version = request.Version;
        if (version != "")
        {
            return Response(MessageId.PayShopInfoResponse, new PayShopInfoResponse
            {
                Version = version,
                ErrorCode = (int)ErrorCode.Success,
                Infos = {
                new PayShopInfo{
                      Id = 3,
                      UpdateTime = 0,
                      Items = {
                             new PayShopItem
                        {
                          Id = 42,
                          TabId = 1,
                          ItemId = 50011,
                          ItemCount = 10000,
                          Locked = false,
                          BuyLimit = 100,
                          BoughtCount = 3,
                          Price = new PayShopPrice
                           {
                                Id = 4,
                                Count = 10,
                                PromotionCount = 5
                            },
                          BeginTime = 0,
                          EndTime = Int64.MaxValue,
                          BeginPromotionTime = 0,
                          EndPromotionTime = Int64.MaxValue,
                          UpdateType = 0,
                          UpdateTime = 0,
                          ShopItemType = 0,
                          Tag = 0,
                          TagBeginTime = 0,
                          TagEndTime = Int64.MaxValue,
                          Sort = 5,
                          GoodsTitle = {new MapField<int, string>{
                              {1, "GoodsTitletest"}
                                }
                           },
                          PromotionShow = 1,
                          CanBuyGoods = true,
                        }
                     }
                },
                new PayShopInfo{
                      Id = 43,
                      UpdateTime = 0,
                      Items = {
                              new PayShopItem
                        {

                        }
                     }
                }

            }
            });
        }
        else
            return Response(MessageId.PayShopInfoResponse, new PayShopInfoResponse
            {
                ErrorCode = (int)ErrorCode.ErrShopVersion
            });

    }

    [NetEvent(MessageId.PayItemRequest)]
    public RpcResult OnPayItemRequest()
    {
        //id_ = other.id_;
        //version_ = other.version_;

        return Response(MessageId.PayItemResponse, new PayItemResponse
        {
            ReceiptId = "123456",
            ErrorCode = (int)ErrorCode.Success
    });
    }

    [NetEvent(MessageId.PayInfoRequest)]
    public RpcResult OnPayInfoRequest()
    {
        return Response(MessageId.PayInfoResponse, new PayInfoResponse
        {
            //Infos
            Version = "1.0.0",
            ErrorCode = (int)ErrorCode.Success
        });
    }

    [NetEvent(MessageId.PayShopBuyRequest)]
    public RpcResult OnPayShopBuyRequest()=>Response(MessageId.PayShopBuyResponse, new PayShopBuyResponse());

    [NetEvent(MessageId.PayShopDirectBuyRequest)]
    public RpcResult OnPayShopDirectBuyRequest()=>Response(MessageId.PayShopDirectBuyResponse, new PayShopDirectBuyResponse());

    [NetEvent(MessageId.PayShopItemUpdateRequest)]
    public RpcResult OnPayShopItemUpdateRequest()=>Response(MessageId.PayShopItemUpdateResponse, new PayShopItemUpdateResponse());

    [NetEvent(MessageId.PayShopUpdateRequest)]
    public RpcResult OnPayShopUpdateRequest()=>Response(MessageId.PayShopUpdateResponse, new PayShopUpdateResponse());

    [NetEvent(MessageId.ShopBuyRequest)]
    public RpcResult OnShopBuyRequest()=>Response(MessageId.ShopBuyResponse, new ShopBuyResponse());

    [NetEvent(MessageId.ShopInfoRequest)]
    public RpcResult OnShopInfoRequest()=>Response(MessageId.ShopInfoResponse, new ShopInfoResponse());

    [NetEvent(MessageId.ShopUpdateRequest)]
    public RpcResult OnShopUpdateRequest()=>Response(MessageId.ShopUpdateResponse, new ShopUpdateResponse());
}

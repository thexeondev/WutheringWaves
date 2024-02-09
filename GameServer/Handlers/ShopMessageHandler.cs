using GameServer.Handlers.Attributes;
using GameServer.Network;
using Protocol;

namespace GameServer.Handlers;
internal class ShopMessageHandler : MessageHandlerBase
{
    public ShopMessageHandler(PlayerSession session) : base(session)
    {
        // ShopMessageHandler.
    }

    [MessageHandler(MessageId.PayShopInfoRequest)]
    public async Task OnPayShopInfoRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.PayShopInfoResponse, new PayShopInfoResponse());
    }
}

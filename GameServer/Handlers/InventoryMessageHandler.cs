using GameServer.Handlers.Attributes;
using GameServer.Network;
using Protocol;

namespace GameServer.Handlers;
internal class InventoryMessageHandler : MessageHandlerBase
{
    public InventoryMessageHandler(PlayerSession session) : base(session)
    {
        // InventoryMessageHandler.
    }

    [MessageHandler(MessageId.NormalItemRequest)]
    public async Task OnNormalItemRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.NormalItemResponse, new NormalItemResponse());
    }

    [MessageHandler(MessageId.WeaponItemRequest)]
    public async Task OnWeaponItemRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.WeaponItemResponse, new WeaponItemResponse());
    }

    [MessageHandler(MessageId.PhantomItemRequest)]
    public async Task OnPhantomItemRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.PhantomItemResponse, new PhantomItemResponse());
    }

    [MessageHandler(MessageId.ItemExchangeInfoRequest)]
    public async Task OnItemExchangeInfoRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.ItemExchangeInfoResponse, new ItemExchangeInfoResponse());
    }
}

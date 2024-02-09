using GameServer.Handlers.Attributes;
using GameServer.Network;
using Protocol;

namespace GameServer.Handlers;
internal class FriendMessageHandler : MessageHandlerBase
{
    public FriendMessageHandler(PlayerSession session) : base(session)
    {
        // FriendMessageHandler.
    }

    [MessageHandler(MessageId.FriendAllRequest)]
    public async Task OnFriendAllRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.FriendAllResponse, new FriendAllResponse());
    }
}

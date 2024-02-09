using GameServer.Handlers.Attributes;
using GameServer.Network;
using Protocol;

namespace GameServer.Handlers;
internal class RoleMessageHandler : MessageHandlerBase
{
    public RoleMessageHandler(KcpSession session) : base(session)
    {
        // RoleMessageHandler.
    }

    [MessageHandler(MessageId.RoleFavorListRequest)]
    public async Task OnRoleFavorListRequest(ReadOnlyMemory<byte> _)
    {
        await Session.Rpc.ReturnAsync(MessageId.RoleFavorListResponse, new RoleFavorListResponse());
    }
}

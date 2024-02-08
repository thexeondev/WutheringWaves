using GameServer.Handlers.Attributes;
using GameServer.Network;
using Protocol;

namespace GameServer.Handlers;
internal class CombatHandler : MessageHandlerBase
{
    public CombatHandler(KcpSession session) : base(session)
    {
    }

    [MessageHandler(MessageId.CombatSendPackRequest)]
    public async Task OnCombatSendPackRequest(ReadOnlyMemory<byte> data)
    {
        CombatSendPackRequest request = CombatSendPackRequest.Parser.ParseFrom(data.Span);

        //Console.WriteLine(request);

        await Session.Rpc.ReturnAsync(MessageId.CombatSendPackResponse, new CombatSendPackResponse
        {
            ErrorCode = (int)ErrorCode.Success
        });
    }
}

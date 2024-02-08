using GameServer.Handlers.Attributes;
using GameServer.Network;
using Protocol;

namespace GameServer.Handlers;
internal class BiographyHandler : MessageHandlerBase
{
    public BiographyHandler(KcpSession session) : base(session)
    {
    }

    [MessageHandler(MessageId.ModifySignatureRequest)]
    public async Task OnModifySignatureRequest(ReadOnlyMemory<byte> data)
    {
        ModifySignatureRequest request = ModifySignatureRequest.Parser.ParseFrom(data.Span);

        Console.WriteLine(request);

        await Session.Rpc.ReturnAsync(MessageId.ModifySignatureResponse, new ModifySignatureResponse
        {
            Signature = request.Signature,
            ErrorCode = ((int)ErrorCode.Success)
        });
    }
}

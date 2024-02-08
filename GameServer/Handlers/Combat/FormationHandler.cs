using GameServer.Handlers.Attributes;
using GameServer.Network;
using Protocol;

namespace GameServer.Handlers;
internal class FormationHandler : MessageHandlerBase
{
    public FormationHandler(KcpSession session) : base(session)
    {
    }

    [MessageHandler(MessageId.FormationChangeRequest)]
    public async Task OnFormationChangeRequest(ReadOnlyMemory<byte> data)
    {
        FormationChangeRequest request = FormationChangeRequest.Parser.ParseFrom(data.Span);
        
        Console.WriteLine(request);

        await Session.Rpc.ReturnAsync(MessageId.FormationChangeResponse, new FormationChangeResponse
        {
            ErrorCode = ((int)ErrorCode.Success),
            ChangeList = { request.ChangeList },
            FormationId = request.FormationId
        });
    }

    [MessageHandler(MessageId.UpdateFormationRequest)]
    public async Task OnUpdateFormationRequest(ReadOnlyMemory<byte> data)
    {
        UpdateFormationRequest request = UpdateFormationRequest.Parser.ParseFrom(data.Span);
        
        Console.WriteLine(request);

        await Session.Rpc.ReturnAsync(MessageId.UpdateFormationResponse, new UpdateFormationResponse
        {
            ErrorCode = ((int)ErrorCode.Success),
            Formation = request.Formation
        });
    }
}

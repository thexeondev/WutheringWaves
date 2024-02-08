using GameServer.Handlers.Attributes;
using GameServer.Network;
using Protocol;

namespace GameServer.Handlers;
internal class TimeHandler : MessageHandlerBase
{
    public TimeHandler(KcpSession session) : base(session)
    {
    }

    [MessageHandler(MessageId.TimeCheckRequest)]
    public async Task OnTimeCheckRequest(ReadOnlyMemory<byte> data)
    {
        TimeCheckRequest request = TimeCheckRequest.Parser.ParseFrom(data.Span);
        
        Console.WriteLine(request);

        await Session.Rpc.ReturnAsync(MessageId.TimeCheckResponse, new TimeCheckResponse
        {
            Code=0
        });
    }
}

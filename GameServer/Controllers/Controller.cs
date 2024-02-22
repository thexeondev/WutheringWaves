global using GameServer.Controllers.Result;
using GameServer.Network;
using GameServer.Network.Messages;
using Google.Protobuf;
using Protocol;

namespace GameServer.Controllers;
internal abstract class Controller
{
    protected PlayerSession Session { get; }

    public Controller(PlayerSession session)
    {
        Session = session;
    }

    protected static RpcResult Response<TProtoBuf>(MessageId messageId, TProtoBuf protoBuf) where TProtoBuf : IMessage<TProtoBuf> => new(new ResponseMessage
    {
        MessageId = messageId,
        Payload = protoBuf.ToByteArray()
    });
}

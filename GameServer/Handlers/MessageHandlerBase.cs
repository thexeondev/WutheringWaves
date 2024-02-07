using GameServer.Network;

namespace GameServer.Handlers;
internal abstract class MessageHandlerBase
{
    protected KcpSession Session { get; }

    public MessageHandlerBase(KcpSession session)
    {
        Session = session;
    }
}

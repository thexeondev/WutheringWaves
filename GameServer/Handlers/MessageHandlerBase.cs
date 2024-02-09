using GameServer.Network;

namespace GameServer.Handlers;
internal abstract class MessageHandlerBase
{
    protected PlayerSession Session { get; }

    public MessageHandlerBase(PlayerSession session)
    {
        Session = session;
    }
}

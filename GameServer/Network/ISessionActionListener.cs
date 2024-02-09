using GameServer.Network.Messages;

namespace GameServer.Network;
internal interface ISessionActionListener
{
    public Task OnServerMessageAvailable(BaseMessage message);
}

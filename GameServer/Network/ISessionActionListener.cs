using GameServer.Network.Packets;

namespace GameServer.Network;
internal interface ISessionActionListener
{
    public Task OnServerMessageAvailable(BaseMessage message);
}

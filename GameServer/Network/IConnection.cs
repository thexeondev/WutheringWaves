using GameServer.Network.Packets;

namespace GameServer.Network;
internal interface IConnection : ISessionActionListener
{
    bool Active { get; }
    ValueTask<BaseMessage?> ReceiveMessageAsync();

}

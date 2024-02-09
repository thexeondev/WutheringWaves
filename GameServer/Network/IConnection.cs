using GameServer.Network.Messages;

namespace GameServer.Network;
internal interface IConnection : ISessionActionListener
{
    bool Active { get; }
    ValueTask<BaseMessage?> ReceiveMessageAsync();

}

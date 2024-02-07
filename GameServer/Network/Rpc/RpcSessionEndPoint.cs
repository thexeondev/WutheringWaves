using GameServer.Network.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace GameServer.Network.Rpc;
internal class RpcSessionEndPoint : IRpcEndPoint
{
    private readonly IServiceProvider _serviceProvider;
    private KcpSession? _session;

    private KcpSession Session => _session ??= _serviceProvider.GetRequiredService<KcpSession>();

    public RpcSessionEndPoint(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task SendRpcResult(ResponseMessage message) => Session.Send(message);
}

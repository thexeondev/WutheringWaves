using GameServer.Network.Messages;
using Microsoft.Extensions.DependencyInjection;

namespace GameServer.Network.Rpc;
internal class RpcSessionEndPoint : IRpcEndPoint
{
    private readonly IServiceProvider _serviceProvider;
    private PlayerSession? _session;

    private PlayerSession Session => _session ??= _serviceProvider.GetRequiredService<PlayerSession>();

    public RpcSessionEndPoint(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task SendRpcResult(ResponseMessage message) => Session.SendRpcRsp(message);
}

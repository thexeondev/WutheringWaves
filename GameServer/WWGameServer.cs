using GameServer.Controllers.Factory;
using GameServer.Network.Kcp;
using Microsoft.Extensions.Hosting;

namespace GameServer;
internal class WWGameServer : IHostedService
{
    private readonly KcpGateway _gateway;

    public WWGameServer(KcpGateway gateway, EventHandlerFactory messageHandlerFactory)
    {
        _ = messageHandlerFactory;
        _gateway = gateway;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _gateway.Start();

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}

using GameServer.Extensions;
using GameServer.Handlers;
using GameServer.Handlers.Factory;
using GameServer.Models;
using GameServer.Network;
using GameServer.Network.Kcp;
using GameServer.Network.Rpc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GameServer;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
        builder.Logging.AddConsole();

        builder.Services.AddHandlers()
                        .AddSingleton<KcpGateway>().AddScoped<PlayerSession>()
                        .AddScoped<MessageManager>().AddSingleton<MessageHandlerFactory>()
                        .AddScoped<RpcManager>().AddScoped<IRpcEndPoint, RpcSessionEndPoint>()
                        .AddSingleton<SessionManager>()
                        .AddScoped<ModelManager>()
                        .AddHostedService<WWGameServer>();

        await builder.Build().RunAsync();
    }
}

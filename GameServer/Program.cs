using GameServer.Extensions;
using GameServer.Handlers;
using GameServer.Handlers.Factory;
using GameServer.Network;
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
                        .AddSingleton<KcpGateway>().AddScoped<KcpSession>()
                        .AddScoped<MessageManager>().AddSingleton<MessageHandlerFactory>()
                        .AddScoped<RpcManager>().AddScoped<IRpcEndPoint, RpcSessionEndPoint>()
                        .AddSingleton<SessionManager>()
                        .AddHostedService<WWGameServer>();

        await builder.Build().RunAsync();
    }
}

using Core.Config;
using Core.Extensions;
using GameServer.Controllers.Factory;
using GameServer.Controllers.Manager;
using GameServer.Extensions;
using GameServer.Models;
using GameServer.Network;
using GameServer.Network.Kcp;
using GameServer.Network.Messages;
using GameServer.Network.Rpc;
using GameServer.Settings;
using GameServer.Systems.Entity;
using GameServer.Systems.Event;
using Microsoft.Extensions.Configuration;
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

        builder.SetupConfiguration();
        builder.Services.UseLocalResources()
                        .AddControllers()
                        .AddSingleton<ConfigManager>()
                        .AddSingleton<KcpGateway>().AddScoped<PlayerSession>()
                        .AddScoped<MessageManager>().AddSingleton<EventHandlerFactory>()
                        .AddScoped<RpcManager>().AddScoped<IRpcEndPoint, RpcSessionEndPoint>()
                        .AddSingleton<SessionManager>()
                        .AddScoped<EventSystem>().AddScoped<EntitySystem>().AddScoped<EntityFactory>()
                        .AddScoped<ModelManager>().AddScoped<ControllerManager>()
                        .AddHostedService<WWGameServer>();

        await builder.Build().RunAsync();
    }

    private static void SetupConfiguration(this HostApplicationBuilder builder)
    {
        builder.Configuration.AddJsonFile("gameplay.json");
        builder.Services.Configure<GatewaySettings>(builder.Configuration.GetRequiredSection("Gateway"));
        builder.Services.Configure<PlayerStartingValues>(builder.Configuration.GetRequiredSection("StartingValues"));
        builder.Services.Configure<GameplayFeatureSettings>(builder.Configuration.GetRequiredSection("Features"));
    }
}

using Core.Config;
using Core.Extensions;
using GameServer.Controllers.ChatCommands;
using GameServer.Controllers.Combat;
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
using GameServer.Systems.Notify;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GameServer;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        Console.Title = "Wuthering Waves | Game Server";
        Console.WriteLine(@"
 __      __        __  .__                 .__                  __      __                            
/  \    /  \__ ___/  |_|  |__   ___________|__| ____    ____   /  \    /  \_____ ___  __ ____   ______
\   \/\/   /  |  \   __\  |  \_/ __ \_  __ \  |/    \  / ___\  \   \/\/   /\__  \\  \/ // __ \ /  ___/
 \        /|  |  /|  | |   Y  \  ___/|  | \/  |   |  \/ /_/  >  \        /  / __ \\   /\  ___/ \___ \ 
  \__/\  / |____/ |__| |___|  /\___  >__|  |__|___|  /\___  /    \__/\  /  (____  /\_/  \___  >____  >
       \/                   \/     \/              \//_____/          \/        \/          \/     \/ 

                                                                                            Game Server");

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Note: This is an open source and free software, please check it out https://discord.gg/reversedrooms");
        Console.ResetColor();
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
        builder.Logging.AddConsole();

        builder.SetupConfiguration();
        GameServer.Extensions.ServiceCollectionExtensions.AddControllers(builder.Services.UseLocalResources());
        builder.Services.UseLocalResources()
                        .AddCommands()
                        .AddSingleton<ConfigManager>()
                        .AddSingleton<KcpGateway>().AddScoped<PlayerSession>()
                        .AddScoped<MessageManager>().AddSingleton<EventHandlerFactory>()
                        .AddScoped<RpcManager>().AddScoped<IRpcEndPoint, RpcSessionEndPoint>()
                        .AddSingleton<SessionManager>()
                        .AddScoped<EventSystem>().AddScoped<EntitySystem>().AddScoped<IGameActionListener, NotifySystem>()
                        .AddScoped<EntityFactory>()
                        .AddScoped<ModelManager>().AddScoped<ControllerManager>()
                        .AddScoped<CombatManager>().AddScoped<ChatCommandManager>()
                        .AddHostedService<WWGameServer>();

        IHost host = builder.Build();

        ILogger logger = host.Services.GetRequiredService<ILoggerFactory>().CreateLogger("WutheringWaves");
        logger.LogInformation("Support: discord.gg/reversedrooms or discord.xeondev.com");
        logger.LogInformation("Preparing server...");

        host.Services.GetRequiredService<IHostApplicationLifetime>().ApplicationStarted.Register(() =>
        {
            logger.LogInformation("Server started! Let's play Wuthering Waves!");
        });

        await host.RunAsync();
    }

    private static void SetupConfiguration(this HostApplicationBuilder builder)
    {
        builder.Configuration.AddJsonFile("data/gameplay.json");
        builder.Services.Configure<GatewaySettings>(builder.Configuration.GetRequiredSection("Gateway"));
        builder.Services.Configure<PlayerStartingValues>(builder.Configuration.GetRequiredSection("StartingValues"));//playerInfo
        builder.Services.Configure<GameplayFeatureSettings>(builder.Configuration.GetRequiredSection("Features"));
    }
}

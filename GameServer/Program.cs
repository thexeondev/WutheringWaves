﻿using GameServer.Controllers.Event;
using GameServer.Controllers.Factory;
using GameServer.Controllers.Manager;
using GameServer.Extensions;
using GameServer.Models;
using GameServer.Network;
using GameServer.Network.Kcp;
using GameServer.Network.Messages;
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

        builder.Services.AddControllers()
                        .AddSingleton<KcpGateway>().AddScoped<PlayerSession>()
                        .AddScoped<MessageManager>().AddSingleton<EventHandlerFactory>()
                        .AddScoped<RpcManager>().AddScoped<IRpcEndPoint, RpcSessionEndPoint>()
                        .AddSingleton<SessionManager>()
                        .AddScoped<EventSystem>().AddScoped<ModelManager>().AddScoped<ControllerManager>()
                        .AddHostedService<WWGameServer>();

        await builder.Build().RunAsync();
    }
}

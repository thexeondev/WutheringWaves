using Microsoft.AspNetCore.Builder;
using SDKServer.Handlers;
using SDKServer.Middleware;

namespace SDKServer;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.WebHost.UseUrls("http://*:5500");
        builder.Logging.AddSimpleConsole();

        var app = builder.Build();
        app.UseMiddleware<NotFoundMiddleware>();

        app.MapGet("/api/login", LoginHandler.Login);
        app.MapGet("/index.json", ConfigHandler.GetBaseConfig);

        app.MapGet("/dev/client/mtZyW6ZYIu1pE0TCHUbXcM1oU8vx4hnb/Windows/KeyList_0.9.0.json", HotPatchHandler.OnKeyListRequest);
        app.MapGet("/dev/client/mtZyW6ZYIu1pE0TCHUbXcM1oU8vx4hnb/Windows/config.json", HotPatchHandler.OnConfigRequest);
        app.MapGet("/dev/client/mtZyW6ZYIu1pE0TCHUbXcM1oU8vx4hnb/Windows/client_key/0.9.0/CtBIsHPiwhwOqqBYxj/PakData", HotPatchHandler.OnPakDataRequest);

        await app.RunAsync();
    }
}

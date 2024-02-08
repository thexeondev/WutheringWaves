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
        app.MapGet("/config/index.json", ConfigHandler.GetBaseConfig);
        
        app.MapGet("/config", TDHandler.Config);
        app.MapPost("/sync", TDHandler.Sync);

        app.MapGet("/dev/client/7cyFLmtLJlUauZ1hM8DsL5Sj7cXxSNQD/Windows/KeyList_0.8.0.json", HotPatchHandler.OnKeyListRequest);
        app.MapGet("/dev/client/7cyFLmtLJlUauZ1hM8DsL5Sj7cXxSNQD/Windows/config.json", HotPatchHandler.OnConfigRequest);
        app.MapGet("/dev/client/7cyFLmtLJlUauZ1hM8DsL5Sj7cXxSNQD/Windows/client_key/0.8.0/xFrH845q3t8Pgy5eB2/PakData", HotPatchHandler.OnPakDataRequest);

        await app.RunAsync();
    }
}

using SDKServer.Handlers;
using SDKServer.Middleware;

namespace SDKServer;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        Console.Title = "Wuthering Waves | SDK Server";
        Console.WriteLine(" __      __        __  .__                 .__                  __      __                            \r\n/  \\    /  \\__ ___/  |_|  |__   ___________|__| ____    ____   /  \\    /  \\_____ ___  __ ____   ______\r\n\\   \\/\\/   /  |  \\   __\\  |  \\_/ __ \\_  __ \\  |/    \\  / ___\\  \\   \\/\\/   /\\__  \\\\  \\/ // __ \\ /  ___/\r\n \\        /|  |  /|  | |   Y  \\  ___/|  | \\/  |   |  \\/ /_/  >  \\        /  / __ \\\\   /\\  ___/ \\___ \\ \r\n  \\__/\\  / |____/ |__| |___|  /\\___  >__|  |__|___|  /\\___  /    \\__/\\  /  (____  /\\_/  \\___  >____  >\r\n       \\/                   \\/     \\/              \\//_____/          \\/        \\/          \\/     \\/ \r\n\r\n\t\t\t\t\t\t\t\t\t\t\t\tSDK Server\n");

        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        builder.WebHost.UseUrls("http://*:5500");
        builder.Logging.AddSimpleConsole();

        WebApplication app = builder.Build();
        app.UseMiddleware<NotFoundMiddleware>();

        app.MapGet("/api/login", LoginHandler.Login);
        app.MapGet("/index.json", ConfigHandler.GetBaseConfig);

        app.MapGet("/dev/client/mtZyW6ZYIu1pE0TCHUbXcM1oU8vx4hnb/Windows/KeyList_0.9.0.json", HotPatchHandler.OnKeyListRequest);
        app.MapGet("/dev/client/mtZyW6ZYIu1pE0TCHUbXcM1oU8vx4hnb/Windows/config.json", HotPatchHandler.OnConfigRequest);
        app.MapGet("/dev/client/mtZyW6ZYIu1pE0TCHUbXcM1oU8vx4hnb/Windows/client_key/0.9.0/CtBIsHPiwhwOqqBYxj/PakData", HotPatchHandler.OnPakDataRequest);

        await app.RunAsync();
    }
}

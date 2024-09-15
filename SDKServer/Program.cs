using SDKServer.Handlers;
using SDKServer.Middleware;


namespace SDKServer;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        Console.Title = "Wuthering Waves | SDK Server";
        Console.WriteLine(@"
 __      __        __  .__                 .__                  __      __                            
/  \    /  \__ ___/  |_|  |__   ___________|__| ____    ____   /  \    /  \_____ ___  __ ____   ______
\   \/\/   /  |  \   __\  |  \_/ __ \_  __ \  |/    \  / ___\  \   \/\/   /\__  \\  \/ // __ \ /  ___/
 \        /|  |  /|  | |   Y  \  ___/|  | \/  |   |  \/ /_/  >  \        /  / __ \\   /\  ___/ \___ \ 
  \__/\  / |____/ |__| |___|  /\___  >__|  |__|___|  /\___  /    \__/\  /  (____  /\_/  \___  >____  >
       \/                   \/     \/              \//_____/          \/        \/          \/     \/ 

                                                                                            SDK Server");
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Note: This is an open source and free software, please check it out https://discord.gg/reversedrooms");
        Console.ResetColor();
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        var config = ConfigManager.GetConfig()!;
        builder.WebHost.UseUrls($"http://*:{config.SDKServer.Port}");
        builder.Logging.AddSimpleConsole();

        WebApplication app = builder.Build();
        app.UseMiddleware<NotFoundMiddleware>();

        app.MapGet("/api/login", LoginHandler.Login);
        app.MapGet("/index.json", ConfigHandler.GetBaseConfig);

        app.MapGet("/Gacha/newplayer.json", GachaHandler.NewPlayerConfig);
        app.MapGet("/Gacha/roleup1.json", GachaHandler.Roleup1Config);
        app.MapGet("/Gacha/roleup2.json", GachaHandler.Roleup2Config);
        app.MapGet("/Gacha/weaponup1.json", GachaHandler.Weaponup1Config);
        app.MapGet("/Gacha/weaponup2.json", GachaHandler.Weaponup2Config);
        app.MapGet("/Gacha/baserole.json", GachaHandler.BaseroleConfig);
        app.MapGet("/Gacha/baseweapon1.json", GachaHandler.Baseweapon1Config);
        app.MapGet("/Gacha/baseweapon2.json", GachaHandler.Baseweapon2Config);
        app.MapGet("/Gacha/baseweapon3.json", GachaHandler.Baseweapon3Config);
        app.MapGet("/Gacha/baseweapon4.json", GachaHandler.Baseweapon4Config);
        app.MapGet("/Gacha/baseweapon5.json", GachaHandler.Baseweapon5Config);

        app.MapGet("/dev/client/mtZyW6ZYIu1pE0TCHUbXcM1oU8vx4hnb/Windows/KeyList_0.9.0.json", HotPatchHandler.OnKeyListRequest);
        app.MapGet("/dev/client/mtZyW6ZYIu1pE0TCHUbXcM1oU8vx4hnb/Windows/config.json", HotPatchHandler.OnConfigRequest);
        app.MapGet("/dev/client/mtZyW6ZYIu1pE0TCHUbXcM1oU8vx4hnb/Windows/client_key/0.9.0/CtBIsHPiwhwOqqBYxj/PakData", HotPatchHandler.OnPakDataRequest);

        await app.RunAsync();
    }
}

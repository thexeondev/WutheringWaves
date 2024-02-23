using Microsoft.AspNetCore.Http.HttpResults;
using SDKServer.Models;

namespace SDKServer.Handlers;

internal static class LoginHandler
{
    public static JsonHttpResult<LoginInfoModel> Login(string token, uint userData)
    {
        var config = SDKServer.GetConfig.ConfigManager.GetConfig()!;
        return TypedResults.Json(new LoginInfoModel
        {
            Code = 0,
            Token = token,
            UserData = userData,
            Host = $"{config.Udp.Ip}",
            Port = config.Udp.Port ,
            HasRpc = true
        });
    }
}

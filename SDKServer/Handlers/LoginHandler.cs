using Microsoft.AspNetCore.Http.HttpResults;
using SDKServer.Models;

namespace SDKServer.Handlers;

internal static class LoginHandler
{
    public static JsonHttpResult<LoginInfoModel> Login(string token, uint userData)
    {
        return TypedResults.Json(new LoginInfoModel
        {
            Code = 0,
            Token = token,
            UserData = userData,
            Host = "127.0.0.1",
            Port = 1337,
            HasRpc = true
        });
    }
}

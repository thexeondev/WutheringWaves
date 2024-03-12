using Microsoft.AspNetCore.Http.HttpResults;
using SDKServer.Models;

namespace SDKServer.Handlers;

internal static class GachaHandler
{
    public static JsonHttpResult<GachaConfigModel> NewPlayerConfig()
    {
        return TypedResults.Json(GachaConfig.GetGachaConfig("newplayer"));
    }
    public static JsonHttpResult<GachaConfigModel> Roleup1Config()
    {
        return TypedResults.Json(GachaConfig.GetGachaConfig("roleup1"));
    }
    public static JsonHttpResult<GachaConfigModel> Roleup2Config()
    {
        return TypedResults.Json(GachaConfig.GetGachaConfig("roleup2"));
    }
    public static JsonHttpResult<GachaConfigModel> Weaponup1Config()
    {
        return TypedResults.Json(GachaConfig.GetGachaConfig("weaponup1"));
    }
    public static JsonHttpResult<GachaConfigModel> Weaponup2Config()
    {
        return TypedResults.Json(GachaConfig.GetGachaConfig("weaponup2"));

    }
    public static JsonHttpResult<GachaConfigModel> BaseroleConfig()
    {
        return TypedResults.Json(GachaConfig.GetGachaConfig("baserole"));

    }
    public static JsonHttpResult<GachaConfigModel> Baseweapon1Config()
    {
        return TypedResults.Json(GachaConfig.GetGachaConfig("baseweapon1"));
    }
    public static JsonHttpResult<GachaConfigModel> Baseweapon2Config()
    {
        return TypedResults.Json(GachaConfig.GetGachaConfig("baseweapon2"));
    }
    public static JsonHttpResult<GachaConfigModel> Baseweapon3Config()
    {
        return TypedResults.Json(GachaConfig.GetGachaConfig("baseweapon3"));
    }
    public static JsonHttpResult<GachaConfigModel> Baseweapon4Config()
    {
        return TypedResults.Json(GachaConfig.GetGachaConfig("baseweapon4"));
    }
    public static JsonHttpResult<GachaConfigModel> Baseweapon5Config()
    {
        return TypedResults.Json(GachaConfig.GetGachaConfig("baseweapon5"));
    }
}

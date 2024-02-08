using Microsoft.AspNetCore.Http.HttpResults;
using SDKServer.Models;

namespace SDKServer.Handlers;

internal static class TDHandler
{
    public static JsonHttpResult<CodeModel> Config()
    {
        return TypedResults.Json(new CodeModel()
        {
            Code = 0,
            Data = new ServerModel()
            {
                ServerTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds(),
                SyncBatchSize = 30,
                SyncInterval = 30
            },
            Msg = ""
        });
    }
    public static JsonHttpResult<SimpleCodeModel> Sync()
    {
        return TypedResults.Json(new SimpleCodeModel()
        {
            Code = 0
        });
    }
}

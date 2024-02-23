using System.IO;
using System.Text.Json;
using Microsoft.AspNetCore.Http.HttpResults;
using SDKServer.Models.BaseConfig;

namespace SDKServer.Handlers;

internal static class ConfigHandler
{
    public static JsonHttpResult<BaseConfigModel> GetBaseConfig()
    {

        var config = GetConfig.ConfigManager.GetConfig()!;

        return TypedResults.Json(new BaseConfigModel
        {
            CdnUrl = [
                new CdnUrlEntry
                {
                    Url = $"http://{config.Http.Ip}:{config.Http.Port}/dev/client/",
                    Weight = "100"
                },
                new CdnUrlEntry
                {
                    Url = $"http://{config.Http.Ip}:{config.Http.Port}/dev/client/",
                    Weight = "100"
                }
            ],
            SecondaryUrl = [],
            GmOpen = true,
            PayUrl = "http://114.132.150.182:12281/ReceiptNotify/PayNotify",
            TDCfg = new TDConfig
            {
                Url = "https://ali-sh-datareceiver.kurogame.xyz",
                AppID = "364c899beea94b92b87ae30869b492d6"
            },
            LogReport = new LogReportConfig
            {
                Ak = "AKIDseIrMkz66ymcSBrjpocFt9IO0lT1SiIk",
                Sk = "MXeeVBfs0ywnleS83xiGczCPVROCnFds",
                Name = "aki-upload-log-1319073642",
                Region = "ap-shanghai"
            },
            NoticUrl = "https://prod-alicdn-gmserver-static.kurogame.com",
            LoginServers = [
                new LoginServerEntry
                {
                    Id = "1000",
                    Name = $"{config.Http.Name}",
                    Ip = $"{config.Http.Ip}"
                }
            ],
            PrivateServers = new PrivateServersConfig
            {
                Enable = false,
                ServerUrl = ""
            }
        });
    }



}

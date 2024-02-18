using System.Text.Json.Serialization;

namespace SDKServer.Models.BaseConfig;

public record BaseConfigModel
{
    [JsonPropertyName("CdnUrl")]
    public required CdnUrlEntry[] CdnUrl { get; set; }

    [JsonPropertyName("SecondaryUrl")]
    public required CdnUrlEntry[] SecondaryUrl { get; set; }

    [JsonPropertyName("SpeedRatio")]
    public int SpeedRatio { get; set; }

    [JsonPropertyName("PriceRatio")]
    public int PriceRatio { get; set; }

    [JsonPropertyName("GmOpen")]
    public bool GmOpen { get; set; }

    [JsonPropertyName("PayUrl")]
    public string PayUrl { get; set; } = string.Empty;

    [JsonPropertyName("TDCfg")]
    public TDConfig? TDCfg { get; set; }

    [JsonPropertyName("LogReport")]
    public LogReportConfig? LogReport { get; set; }

    [JsonPropertyName("NoticUrl")]
    public string NoticUrl { get; set; } = string.Empty;

    [JsonPropertyName("LoginServers")]
    public required LoginServerEntry[] LoginServers { get; set; }

    [JsonPropertyName("PrivateServers")]
    public required PrivateServersConfig PrivateServers { get; set; }
}

using System.Text.Json.Serialization;

namespace SDKServer.Models.BaseConfig;

public record TDConfig
{
    [JsonPropertyName("URL")]
    public required string Url { get; set; }

    [JsonPropertyName("AppID")]
    public required string AppID { get; set; }
}

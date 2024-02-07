using System.Text.Json.Serialization;

namespace SDKServer.Models.BaseConfig;

public record CdnUrlEntry
{
    [JsonPropertyName("url")]
    public required string Url { get; set; }

    [JsonPropertyName("weight")]
    public string Weight { get; set; } = "100";
}

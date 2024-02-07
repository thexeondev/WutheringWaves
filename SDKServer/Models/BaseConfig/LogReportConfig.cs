using System.Text.Json.Serialization;

namespace SDKServer.Models.BaseConfig;

public record LogReportConfig
{
    [JsonPropertyName("ak")]
    public required string Ak { get; set; }

    [JsonPropertyName("sk")]
    public required string Sk { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("region")]
    public required string Region { get; set; }
}

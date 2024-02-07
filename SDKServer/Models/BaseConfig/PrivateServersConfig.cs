using System.Text.Json.Serialization;

namespace SDKServer.Models.BaseConfig;

public record PrivateServersConfig
{
    [JsonPropertyName("enable")]
    public bool Enable { get; set; }

    [JsonPropertyName("serverUrl")]
    public string ServerUrl { get; set; } = string.Empty;
}
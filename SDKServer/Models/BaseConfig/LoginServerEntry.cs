using System.Text.Json.Serialization;

namespace SDKServer.Models.BaseConfig;

public record LoginServerEntry
{
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("ip")]
    public required string Ip { get; set; }
}

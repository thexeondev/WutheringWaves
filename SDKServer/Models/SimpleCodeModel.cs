using System.Text.Json.Serialization;

namespace SDKServer.Models;

public record SimpleCodeModel
{
    [JsonPropertyName("code")]
    public required int Code { get; set; }
}

using System.Text.Json.Serialization;

namespace SDKServer.Models;

public record CodeModel
{
    [JsonPropertyName("code")]
    public required int Code { get; set; }

    [JsonPropertyName("data")]
    public required ServerModel Data { get; set; }

    [JsonPropertyName("msg")]
    public required string Msg { get; set; }
}

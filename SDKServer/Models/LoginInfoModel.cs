using System.Text.Json.Serialization;

namespace SDKServer.Models;

public record LoginInfoModel
{
    [JsonPropertyName("token")]
    public required string Token { get; set; }

    [JsonPropertyName("host")]
    public required string Host { get; set; }

    [JsonPropertyName("port")]
    public required int Port { get; set; }

    [JsonPropertyName("code")]
    public required int Code { get; set; }

    [JsonPropertyName("userData")]
    public required uint UserData { get; set; }

    [JsonPropertyName("hasRpc")]
    public bool HasRpc { get; set; }

    [JsonPropertyName("errMessage")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ErrMessage { get; set; }
}

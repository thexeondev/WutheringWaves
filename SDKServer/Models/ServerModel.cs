using System.Text.Json.Serialization;

namespace SDKServer.Models;

public record ServerModel
{
    [JsonPropertyName("server_timestamp")]
    public required long ServerTimestamp { get; set; }

    [JsonPropertyName("sync_batch_size")]
    public required int SyncBatchSize { get; set; }

    [JsonPropertyName("sync_interval")]
    public required int SyncInterval { get; set; }
}

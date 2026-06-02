using System.Text.Json.Serialization;

namespace UkraineAlarm;

/// <summary>A single active alert within a region.</summary>
public sealed record Alert
{
    /// <summary>Identifier of the region the alert belongs to.</summary>
    [JsonPropertyName("regionId")]
    public string? RegionId { get; init; }

    /// <summary>Administrative level of the region.</summary>
    [JsonPropertyName("regionType")]
    public V2RegionType? RegionType { get; init; }

    /// <summary>Type of the alert.</summary>
    [JsonPropertyName("type")]
    public AlertType? Type { get; init; }

    /// <summary>Timestamp of the last update of the alert.</summary>
    [JsonPropertyName("lastUpdate")]
    public DateTimeOffset? LastUpdate { get; init; }
}

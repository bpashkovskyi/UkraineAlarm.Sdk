using System.Text.Json.Serialization;

namespace UkraineAlarm;

/// <summary>Region together with its currently active alerts.</summary>
public sealed record AlertRegionModel
{
    /// <summary>Region identifier.</summary>
    [JsonPropertyName("regionId")]
    public string? RegionId { get; init; }

    /// <summary>Administrative level of the region.</summary>
    [JsonPropertyName("regionType")]
    public V2RegionType? RegionType { get; init; }

    /// <summary>Region name (Ukrainian).</summary>
    [JsonPropertyName("regionName")]
    public string? RegionName { get; init; }

    /// <summary>Region name (English).</summary>
    [JsonPropertyName("regionEngName")]
    public string? RegionEngName { get; init; }

    /// <summary>Timestamp of the last update.</summary>
    [JsonPropertyName("lastUpdate")]
    public DateTimeOffset? LastUpdate { get; init; }

    /// <summary>Alerts that are currently active in the region.</summary>
    [JsonPropertyName("activeAlerts")]
    public IReadOnlyList<Alert> ActiveAlerts { get; init; } = [];
}

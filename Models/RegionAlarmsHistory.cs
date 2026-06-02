using System.Text.Json.Serialization;

namespace UkraineAlarm;

/// <summary>History of alarms for a region (last 25 records).</summary>
public sealed record RegionAlarmsHistory
{
    /// <summary>Region identifier.</summary>
    [JsonPropertyName("regionId")]
    public string? RegionId { get; init; }

    /// <summary>Region name.</summary>
    [JsonPropertyName("regionName")]
    public string? RegionName { get; init; }

    /// <summary>Historical alarm records.</summary>
    [JsonPropertyName("alarms")]
    public IReadOnlyList<RegionAlarmModel> Alarms { get; init; } = [];
}

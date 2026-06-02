using System.Text.Json.Serialization;

namespace UkraineAlarm;

/// <summary>A historical alarm record for a region.</summary>
public sealed record RegionAlarmModel
{
    /// <summary>Region identifier.</summary>
    [JsonPropertyName("regionId")]
    public string? RegionId { get; init; }

    /// <summary>Moment the alarm started.</summary>
    [JsonPropertyName("startDate")]
    public DateTimeOffset? StartDate { get; init; }

    /// <summary>Moment the alarm ended.</summary>
    [JsonPropertyName("endDate")]
    public DateTimeOffset? EndDate { get; init; }

    /// <summary>Duration of the alarm.</summary>
    [JsonPropertyName("duration")]
    [JsonConverter(typeof(FlexibleTimeSpanConverter))]
    public TimeSpan? Duration { get; init; }

    /// <summary>Type of the alarm.</summary>
    [JsonPropertyName("alertType")]
    public AlertType? AlertType { get; init; }

    /// <summary>Region name.</summary>
    [JsonPropertyName("regionName")]
    public string? RegionName { get; init; }

    /// <summary>Whether the alarm is still ongoing.</summary>
    [JsonPropertyName("isContinue")]
    public bool IsContinue { get; init; }
}

using System.Text.Json.Serialization;

namespace UkraineAlarm;

/// <summary>A region node, possibly containing child regions.</summary>
public sealed record RegionViewModel
{
    /// <summary>Region identifier.</summary>
    [JsonPropertyName("regionId")]
    public string? RegionId { get; init; }

    /// <summary>Region name.</summary>
    [JsonPropertyName("regionName")]
    public string? RegionName { get; init; }

    /// <summary>Administrative level of the region.</summary>
    [JsonPropertyName("regionType")]
    public V2RegionType? RegionType { get; init; }

    /// <summary>Child regions (districts, communities).</summary>
    [JsonPropertyName("regionChildIds")]
    public IReadOnlyList<RegionViewModel> RegionChildIds { get; init; } = [];
}

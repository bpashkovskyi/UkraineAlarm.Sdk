using System.Text.Json.Serialization;

namespace UkraineAlarm;

/// <summary>Full list of regions grouped by states.</summary>
public sealed record RegionsViewModel
{
    /// <summary>Top-level states (oblasts).</summary>
    [JsonPropertyName("states")]
    public IReadOnlyList<RegionViewModel> States { get; init; } = [];
}

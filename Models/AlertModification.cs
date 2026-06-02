using System.Text.Json.Serialization;

namespace UkraineAlarm;

/// <summary>Result of the status (last action index) endpoint.</summary>
public sealed record AlertModification
{
    /// <summary>Monotonically increasing index of the last data modification.</summary>
    [JsonPropertyName("lastActionIndex")]
    public long LastActionIndex { get; init; }
}

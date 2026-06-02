using System.Text.Json.Serialization;

namespace UkraineAlarm;

/// <summary>Administrative level of a region.</summary>
[JsonConverter(typeof(JsonStringEnumConverter<V2RegionType>))]
public enum V2RegionType
{
    /// <summary>No / unknown region type.</summary>
    [JsonStringEnumMemberName("Null")]
    Null,

    /// <summary>State (oblast).</summary>
    [JsonStringEnumMemberName("State")]
    State,

    /// <summary>District (raion).</summary>
    [JsonStringEnumMemberName("District")]
    District,

    /// <summary>Community (hromada).</summary>
    [JsonStringEnumMemberName("Community")]
    Community
}

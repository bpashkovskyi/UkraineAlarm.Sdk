using System.Text.Json.Serialization;

namespace UkraineAlarm;

/// <summary>Type of an active alert.</summary>
[JsonConverter(typeof(JsonStringEnumConverter<AlertType>))]
public enum AlertType
{
    /// <summary>Unknown alert type.</summary>
    [JsonStringEnumMemberName("UNKNOWN")]
    Unknown,

    /// <summary>Air raid alert.</summary>
    [JsonStringEnumMemberName("AIR")]
    Air,

    /// <summary>Artillery shelling threat.</summary>
    [JsonStringEnumMemberName("ARTILLERY")]
    Artillery,

    /// <summary>Urban (street) fights.</summary>
    [JsonStringEnumMemberName("URBAN_FIGHTS")]
    UrbanFights,

    /// <summary>Chemical threat.</summary>
    [JsonStringEnumMemberName("CHEMICAL")]
    Chemical,

    /// <summary>Nuclear threat.</summary>
    [JsonStringEnumMemberName("NUCLEAR")]
    Nuclear,

    /// <summary>Informational message.</summary>
    [JsonStringEnumMemberName("INFO")]
    Info
}

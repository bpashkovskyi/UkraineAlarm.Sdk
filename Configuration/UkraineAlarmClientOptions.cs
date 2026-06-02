using System.ComponentModel.DataAnnotations;

namespace UkraineAlarm;

/// <summary>Configuration for <see cref="UkraineAlarmClient"/>.</summary>
public sealed class UkraineAlarmClientOptions
{
    /// <summary>Configuration section name used by the IOptions pattern.</summary>
    public const string SectionName = "UkraineAlarm";

    /// <summary>Base address of the Ukraine Alert API.</summary>
    public Uri BaseAddress { get; set; } = new("https://api.ukrainealarm.com");

    /// <summary>API key sent in the <c>Authorization</c> header. Required.</summary>
    [Required]
    public string ApiKey { get; set; } = string.Empty;
}

using System.Text.Json.Serialization;

namespace UkraineAlarm;

/// <summary>Payload for webhook subscription endpoints.</summary>
public sealed record WebHookModel
{
    /// <summary>URL that receives alert notifications.</summary>
    [JsonPropertyName("webHookUrl")]
    public string? WebHookUrl { get; init; }
}

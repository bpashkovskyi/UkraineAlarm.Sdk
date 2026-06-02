using System.Net;

namespace UkraineAlarm;

/// <summary>Thrown when the Ukraine Alert API responds with a non-success status code.</summary>
public sealed class UkraineAlarmApiException : Exception
{
    /// <summary>HTTP status code returned by the API.</summary>
    public HttpStatusCode StatusCode { get; }

    /// <summary>Raw response body, if any.</summary>
    public string? ResponseBody { get; }

    /// <summary>Creates a new <see cref="UkraineAlarmApiException"/>.</summary>
    public UkraineAlarmApiException(HttpStatusCode statusCode, string? responseBody)
        : base($"Ukraine Alert API request failed with status {(int)statusCode} ({statusCode}).")
    {
        StatusCode = statusCode;
        ResponseBody = responseBody;
    }
}

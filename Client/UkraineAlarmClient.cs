using System.Net.Http.Json;
using System.Text.Json;

namespace UkraineAlarm;

/// <summary>Default <see cref="IUkraineAlarmClient"/> implementation backed by <see cref="HttpClient"/>.</summary>
public sealed class UkraineAlarmClient(HttpClient httpClient) : IUkraineAlarmClient
{
    internal static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    private readonly HttpClient _httpClient = httpClient;

    /// <inheritdoc />
    public Task<IReadOnlyList<AlertRegionModel>> GetAlertsAsync(CancellationToken cancellationToken = default) =>
        GetAsync<IReadOnlyList<AlertRegionModel>>("api/v3/alerts", cancellationToken);

    /// <inheritdoc />
    public Task<IReadOnlyList<AlertRegionModel>> GetRegionAlertsAsync(string regionId, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(regionId);
        return GetAsync<IReadOnlyList<AlertRegionModel>>($"api/v3/alerts/{Uri.EscapeDataString(regionId)}", cancellationToken);
    }

    /// <inheritdoc />
    public Task<IReadOnlyList<RegionAlarmsHistory>> GetRegionHistoryAsync(string? regionId = null, CancellationToken cancellationToken = default)
    {
        string path = string.IsNullOrWhiteSpace(regionId)
            ? "api/v3/alerts/regionHistory"
            : $"api/v3/alerts/regionHistory?regionId={Uri.EscapeDataString(regionId)}";
        return GetAsync<IReadOnlyList<RegionAlarmsHistory>>(path, cancellationToken);
    }

    /// <inheritdoc />
    public Task<AlertModification> GetStatusAsync(CancellationToken cancellationToken = default) =>
        GetAsync<AlertModification>("api/v3/alerts/status", cancellationToken);

    /// <inheritdoc />
    public Task<RegionsViewModel> GetRegionsAsync(CancellationToken cancellationToken = default) =>
        GetAsync<RegionsViewModel>("api/v3/regions", cancellationToken);

    /// <inheritdoc />
    public async Task<AlertRegionModel> CreateWebhookAsync(WebHookModel webhook, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(webhook);
        using HttpResponseMessage response = await _httpClient
            .PostAsJsonAsync("api/v3/webhook", webhook, JsonOptions, cancellationToken)
            .ConfigureAwait(false);
        return await ReadAsync<AlertRegionModel>(response, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task UpdateWebhookAsync(WebHookModel webhook, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(webhook);
        using HttpResponseMessage response = await _httpClient
            .PatchAsJsonAsync("api/v3/webhook", webhook, JsonOptions, cancellationToken)
            .ConfigureAwait(false);
        await EnsureSuccessAsync(response, cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task DeleteWebhookAsync(WebHookModel webhook, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(webhook);
        using HttpRequestMessage request = new(HttpMethod.Delete, "api/v3/webhook")
        {
            Content = JsonContent.Create(webhook, options: JsonOptions)
        };
        using HttpResponseMessage response = await _httpClient
            .SendAsync(request, cancellationToken)
            .ConfigureAwait(false);
        await EnsureSuccessAsync(response, cancellationToken).ConfigureAwait(false);
    }

    private async Task<T> GetAsync<T>(string path, CancellationToken cancellationToken)
    {
        using HttpResponseMessage response = await _httpClient
            .GetAsync(path, cancellationToken)
            .ConfigureAwait(false);
        return await ReadAsync<T>(response, cancellationToken).ConfigureAwait(false);
    }

    private static async Task<T> ReadAsync<T>(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        await EnsureSuccessAsync(response, cancellationToken).ConfigureAwait(false);
        T? result = await response.Content
            .ReadFromJsonAsync<T>(JsonOptions, cancellationToken)
            .ConfigureAwait(false);
        return result ?? throw new UkraineAlarmApiException(response.StatusCode, "The API returned an empty response body.");
    }

    private static async Task EnsureSuccessAsync(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        if (response.IsSuccessStatusCode)
        {
            return;
        }

        string? body = await response.Content
            .ReadAsStringAsync(cancellationToken)
            .ConfigureAwait(false);
        throw new UkraineAlarmApiException(response.StatusCode, body);
    }
}

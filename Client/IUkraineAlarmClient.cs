namespace UkraineAlarm;

/// <summary>Client for the Ukraine Alert API (api.ukrainealarm.com, API v3).</summary>
public interface IUkraineAlarmClient
{
    /// <summary>Gets all regions that currently have active alerts.</summary>
    Task<IReadOnlyList<AlertRegionModel>> GetAlertsAsync(CancellationToken cancellationToken = default);

    /// <summary>Gets the current alert status of a specific region, district or community.</summary>
    /// <param name="regionId">Identifier of the region/district/community.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    Task<IReadOnlyList<AlertRegionModel>> GetRegionAlertsAsync(string regionId, CancellationToken cancellationToken = default);

    /// <summary>Gets the last 25 alarms for a region (or all regions when <paramref name="regionId"/> is null).</summary>
    /// <param name="regionId">Optional region identifier.</param>
    /// <param name="cancellationToken">Token to cancel the request.</param>
    Task<IReadOnlyList<RegionAlarmsHistory>> GetRegionHistoryAsync(string? regionId = null, CancellationToken cancellationToken = default);

    /// <summary>Gets the last action index, used to decide whether cached data needs refreshing.</summary>
    Task<AlertModification> GetStatusAsync(CancellationToken cancellationToken = default);

    /// <summary>Gets the full list of states, regions and cities.</summary>
    Task<RegionsViewModel> GetRegionsAsync(CancellationToken cancellationToken = default);

    /// <summary>Subscribes a webhook to alert notifications.</summary>
    Task<AlertRegionModel> CreateWebhookAsync(WebHookModel webhook, CancellationToken cancellationToken = default);

    /// <summary>Updates an existing webhook subscription.</summary>
    Task UpdateWebhookAsync(WebHookModel webhook, CancellationToken cancellationToken = default);

    /// <summary>Removes a webhook subscription.</summary>
    Task DeleteWebhookAsync(WebHookModel webhook, CancellationToken cancellationToken = default);
}

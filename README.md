# UkraineAlarm.Sdk

Unofficial **.NET 10** SDK for the [Ukraine Alert API](https://api.ukrainealarm.com/swagger/index.html) (`api.ukrainealarm.com`, API **v3**).

The API requires an API key. Request one via the form at [api.ukrainealarm.com](https://api.ukrainealarm.com/).

## Install

Add a project/package reference to `UkraineAlarm.Sdk`.

## Usage with dependency injection

```csharp
using UkraineAlarm;

builder.Services.AddUkraineAlarmClient("YOUR_API_KEY");
// or bind from configuration ("UkraineAlarm" section with ApiKey / BaseAddress):
// builder.Services.AddUkraineAlarmClient(builder.Configuration.GetSection(UkraineAlarmClientOptions.SectionName));
```

```csharp
public sealed class AlertsService(IUkraineAlarmClient client)
{
    public async Task PrintActiveAlertsAsync(CancellationToken ct)
    {
        IReadOnlyList<AlertRegionModel> regions = await client.GetAlertsAsync(ct);
        foreach (AlertRegionModel region in regions)
        {
            Console.WriteLine($"{region.RegionName}: {region.ActiveAlerts.Count} active alert(s)");
        }
    }
}
```

## Usage without a container

```csharp
using UkraineAlarm;

using HttpClient http = new() { BaseAddress = new Uri("https://api.ukrainealarm.com") };
http.DefaultRequestHeaders.Add("Authorization", "YOUR_API_KEY");

IUkraineAlarmClient client = new UkraineAlarmClient(http);
AlertModification status = await client.GetStatusAsync();
Console.WriteLine(status.LastActionIndex);
```

## Endpoints

| Method | Description |
| --- | --- |
| `GetAlertsAsync()` | `GET /api/v3/alerts` — all regions with active alerts |
| `GetRegionAlertsAsync(regionId)` | `GET /api/v3/alerts/{regionId}` — status of a region/district/community |
| `GetRegionHistoryAsync(regionId?)` | `GET /api/v3/alerts/regionHistory` — last 25 alarms |
| `GetStatusAsync()` | `GET /api/v3/alerts/status` — last action index |
| `GetRegionsAsync()` | `GET /api/v3/regions` — full region tree |
| `CreateWebhookAsync(model)` | `POST /api/v3/webhook` |
| `UpdateWebhookAsync(model)` | `PATCH /api/v3/webhook` |
| `DeleteWebhookAsync(model)` | `DELETE /api/v3/webhook` |

Non-success responses throw `UkraineAlarmApiException` (exposes `StatusCode` and `ResponseBody`).

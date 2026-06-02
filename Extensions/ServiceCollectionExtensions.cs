using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace UkraineAlarm;

/// <summary>Registration helpers for <see cref="IUkraineAlarmClient"/>.</summary>
public static class ServiceCollectionExtensions
{
    /// <summary>Registers <see cref="IUkraineAlarmClient"/> as a typed <see cref="HttpClient"/> using the provided API key.</summary>
    public static IHttpClientBuilder AddUkraineAlarmClient(this IServiceCollection services, string apiKey)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentException.ThrowIfNullOrWhiteSpace(apiKey);
        return services.AddUkraineAlarmClient(options => options.ApiKey = apiKey);
    }

    /// <summary>Registers <see cref="IUkraineAlarmClient"/> binding options from the given configuration section.</summary>
    public static IHttpClientBuilder AddUkraineAlarmClient(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);
        services.Configure<UkraineAlarmClientOptions>(configuration);
        return services.AddUkraineAlarmClientCore();
    }

    /// <summary>Registers <see cref="IUkraineAlarmClient"/> with an inline options configuration callback.</summary>
    public static IHttpClientBuilder AddUkraineAlarmClient(this IServiceCollection services, Action<UkraineAlarmClientOptions> configure)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configure);
        services.Configure(configure);
        return services.AddUkraineAlarmClientCore();
    }

    private static IHttpClientBuilder AddUkraineAlarmClientCore(this IServiceCollection services)
    {
        services.AddOptions<UkraineAlarmClientOptions>()
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services.AddHttpClient<IUkraineAlarmClient, UkraineAlarmClient>(static (provider, client) =>
        {
            UkraineAlarmClientOptions options = provider
                .GetRequiredService<IOptions<UkraineAlarmClientOptions>>()
                .Value;

            client.BaseAddress = options.BaseAddress;
            client.DefaultRequestHeaders.Add("Authorization", options.ApiKey);
        });
    }
}

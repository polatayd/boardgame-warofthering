using BoardGame.WarOfTheRing.Fellowships.Application.Services;
using BoardGame.WarOfTheRing.Fellowships.Infrastructure.Services;
using BoardGame.WarOfTheRing.Fellowships.Infrastructure.Services.Dice;
using BoardGame.WarOfTheRing.Fellowships.Infrastructure.Services.Map;
using Microsoft.Extensions.Http.Resilience;
using Polly;

namespace BoardGame.WarOfTheRing.Fellowships.Api.ServiceRegistrations;

public static class HttpClientServiceRegistration
{
    public static IServiceCollection RegisterHttpClientServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var servicesOptions = configuration.GetSection(ServicesOptions.Services).Get<ServicesOptions>();

        services.AddTransient<LoggingDelegatingHandler>();

        var diceOptions = servicesOptions.Dice;
        services.AddHttpClient<IDiceService, DiceApiClient>().ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(diceOptions.BaseAddress);
                client.Timeout = TimeSpan.FromMilliseconds(diceOptions.Timeout);
            })
            .AddHttpMessageHandler<LoggingDelegatingHandler>()
            .AddResilienceHandler("dice-pipeline",
                pb => ApiClientResilienceHandler(pb, diceOptions));

        var mapOptions = servicesOptions.Map;
        services.AddHttpClient<IMapService, MapApiClient>().ConfigureHttpClient(client =>
            {
                client.BaseAddress = new Uri(mapOptions.BaseAddress);
                client.Timeout = TimeSpan.FromMilliseconds(mapOptions.Timeout);
            })
            .AddHttpMessageHandler<LoggingDelegatingHandler>()
            .AddResilienceHandler("map-pipeline",
                pb => ApiClientResilienceHandler(pb, mapOptions));

        return services;
    }

    private static void ApiClientResilienceHandler(ResiliencePipelineBuilder<HttpResponseMessage> pipelineBuilder,
        ResiliencyOptions resiliencyOptions)
    {
        var retryStrategy = resiliencyOptions.RetryStrategy;
        if (retryStrategy.MaxRetryAttempts != 0)
        {
            pipelineBuilder.AddRetry(new HttpRetryStrategyOptions
            {
                MaxRetryAttempts = retryStrategy.MaxRetryAttempts,
                BackoffType = Enum.TryParse(retryStrategy.BackoffType, out DelayBackoffType backoffType)
                    ? backoffType
                    : DelayBackoffType.Constant,
                Delay = TimeSpan.FromMilliseconds(retryStrategy.Delay)
            });
        }

        pipelineBuilder.Build();
    }
}
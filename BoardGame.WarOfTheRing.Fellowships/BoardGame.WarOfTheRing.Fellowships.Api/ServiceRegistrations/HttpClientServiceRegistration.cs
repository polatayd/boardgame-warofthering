using BoardGame.WarOfTheRing.Fellowships.Application.Services;
using BoardGame.WarOfTheRing.Fellowships.Infrastructure.Services.Dice;
using Microsoft.Extensions.Http.Resilience;
using Polly;

namespace BoardGame.WarOfTheRing.Fellowships.Api.ServiceRegistrations;

public static class HttpClientServiceRegistration
{
    public static IServiceCollection RegisterHttpClientServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var servicesOptions = configuration.GetSection(ServicesOptions.Services).Get<ServicesOptions>();
        var diceOptions = servicesOptions.Dice;

        ILogger<DiceApiClient> diceApiLogger = null;

        services.AddHttpClient<IDiceService, DiceApiClient>().ConfigureHttpClient((serviceProvider, client) =>
        {
            diceApiLogger = serviceProvider.GetRequiredService<ILogger<DiceApiClient>>();

            client.BaseAddress = new Uri(diceOptions.BaseAddress);
            client.Timeout = TimeSpan.FromMilliseconds(diceOptions.Timeout);
        }).AddResilienceHandler("dice-pipeline",
            pb => DiceApiClientResilienceHandler(pb, diceOptions, diceApiLogger));

        return services;
    }

    private static void DiceApiClientResilienceHandler(ResiliencePipelineBuilder<HttpResponseMessage> pipelineBuilder,
        DiceOptions diceOptions, ILogger<DiceApiClient> logger)
    {
        var retryStrategy = diceOptions.RetryStrategy;

        pipelineBuilder.AddRetry(new HttpRetryStrategyOptions
        {
            MaxRetryAttempts = retryStrategy.MaxRetryAttempts,
            BackoffType = Enum.TryParse(retryStrategy.BackoffType, out DelayBackoffType backoffType)
                ? backoffType
                : DelayBackoffType.Constant,
            Delay = TimeSpan.FromMilliseconds(retryStrategy.Delay),
            OnRetry = arguments =>
            {
                logger.LogError("Message:{Message}",
                    $"Dice Api Retry Attempt {arguments.AttemptNumber} failed");

                return ValueTask.CompletedTask;
            }
        });

        pipelineBuilder.Build();
    }
}
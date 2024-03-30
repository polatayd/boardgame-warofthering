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

        services.AddHttpClient<IDiceService, DiceApiClient>().ConfigureHttpClient(client =>
        {
            client.BaseAddress = new Uri(diceOptions.BaseAddress);
            client.Timeout = TimeSpan.FromMilliseconds(diceOptions.Timeout);
        }).AddResilienceHandler("dice-pipeline",
            pb => DiceApiClientResilienceHandler(pb, diceOptions));

        return services;
    }

    private static void DiceApiClientResilienceHandler(ResiliencePipelineBuilder<HttpResponseMessage> pipelineBuilder,
        DiceOptions diceOptions)
    {
        var retryStrategy = diceOptions.RetryStrategy;

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
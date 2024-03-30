using BoardGame.WarOfTheRing.Fellowships.Application.Services;
using BoardGame.WarOfTheRing.Fellowships.Infrastructure.Services.Dice;

namespace BoardGame.WarOfTheRing.Fellowships.Api.ServiceRegistrations;

public static class HttpClientServiceRegistration
{
    //TODO: Add Polly support.
    public static IServiceCollection RegisterHttpClientServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var servicesConfiguration = configuration.GetSection(ServicesOptions.Services).Get<ServicesOptions>();
        
        services.AddHttpClient<IDiceService, DiceApiClient>(client =>
        {
            client.BaseAddress = new Uri(servicesConfiguration.Dice.BaseAddress);
            client.Timeout = TimeSpan.FromMilliseconds(servicesConfiguration.Dice.Timeout);
        });

        return services;
    }
}
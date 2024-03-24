using System.Reflection;
using System.Text.Json.Serialization;
using BoardGame.WarOfTheRing.PoliticalTrack.Application;
using BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations;
using BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations.Commands;
using BoardGame.WarOfTheRing.PoliticalTrack.Infrastructure.Persistence.EntityFrameworkCore;
using BoardGame.WarOfTheRing.PoliticalTrack.Infrastructure.Persistence.EntityFrameworkCore.Nations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BoardGame.WarOfTheRing.PoliticalTrack.Api.ServiceRegistrations;

public static class PoliticalTrackServiceRegistration
{
    public static IServiceCollection RegisterPoliticalTrackServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.ConfigureHttpJsonOptions(options =>
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));
        services.Configure<JsonOptions>(options =>
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(CreateNationCommand).GetTypeInfo().Assembly));

        services.AddDbContext<PoliticalTrackDbContext>((_, optionsBuilder) =>
                optionsBuilder.UseNpgsql(configuration.GetConnectionString("postgresql"))
                    .EnableSensitiveDataLogging())
            .AddScoped<IUnitOfWork, PoliticalTrackDbContext>(x => x.GetRequiredService<PoliticalTrackDbContext>());

        services.AddScoped<INationRepository, NationRepository>();

        services.AddProblemDetails();

        return services;
    }
}
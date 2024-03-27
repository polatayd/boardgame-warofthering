using System.Reflection;
using System.Text.Json.Serialization;
using BoardGame.WarOfTheRing.Fellowships.Application;
using BoardGame.WarOfTheRing.Fellowships.Application.Fellowships;
using BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.Commands;
using BoardGame.WarOfTheRing.Fellowships.Application.Hunts;
using BoardGame.WarOfTheRing.Fellowships.Infrastructure.DomainEventDispatcher;
using BoardGame.WarOfTheRing.Fellowships.Infrastructure.Persistence.EntityFrameworkCore;
using BoardGame.WarOfTheRing.Fellowships.Infrastructure.Persistence.EntityFrameworkCore.Fellowships;
using BoardGame.WarOfTheRing.Fellowships.Infrastructure.Persistence.EntityFrameworkCore.Hunts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BoardGame.WarOfTheRing.Fellowships.Api.ServiceRegistrations;

public static class FellowshipServiceRegistration
{
    public static IServiceCollection RegisterFellowshipServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.ConfigureHttpJsonOptions(options =>
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));
        services.Configure<JsonOptions>(options =>
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(CreateFellowshipCommand).GetTypeInfo().Assembly));

        services.AddDbContext<FellowshipDbContext>((_, optionsBuilder) =>
                optionsBuilder.UseNpgsql(configuration.GetConnectionString("postgresql"))
                    .EnableSensitiveDataLogging())
            .AddScoped<IUnitOfWork, FellowshipDbContext>(x => x.GetRequiredService<FellowshipDbContext>());

        services.AddScoped<IFellowshipRepository, FellowshipRepository>();
        services.AddScoped<IHuntRepository, HuntRepository>();
        services.AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>();

        services.AddProblemDetails();

        return services;
    }
}
using System.Reflection;
using System.Text.Json.Serialization;
using BoardGame.WarOfTheRing.Dice.Application.DicePools.Queries;
using BoardGame.WarOfTheRing.Dice.Application.DicePools.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BoardGame.WarOfTheRing.Dice.Api.ServiceRegistrations;

public static class DiceServiceRegistration
{
    public static IServiceCollection RegisterPoliticalTrackServices(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.ConfigureHttpJsonOptions(options =>
            options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));
        services.Configure<JsonOptions>(options =>
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(RollDicePoolRequest).GetTypeInfo().Assembly));

        services.AddValidatorsFromAssemblyContaining<RollDicePoolInputValidator>();

        services.AddProblemDetails();

        return services;
    }
}
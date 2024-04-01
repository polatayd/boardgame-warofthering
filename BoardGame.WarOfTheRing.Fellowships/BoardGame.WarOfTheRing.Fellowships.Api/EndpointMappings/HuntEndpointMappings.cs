using BoardGame.WarOfTheRing.Fellowships.Api.EndpointHandlers;

namespace BoardGame.WarOfTheRing.Fellowships.Api.EndpointMappings;

public static class HuntEndpointMappings
{
    public static void RegisterHuntEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var huntEndpoints = endpointRouteBuilder.MapGroup("/hunts/{gameId}");

        huntEndpoints.MapPost("/roll", HuntHandlers.RollDice)
            .WithName("RollDice")
            .WithOpenApi();
        
        huntEndpoints.MapPost("/reroll", HuntHandlers.ReRollDice)
            .WithName("ReRollDice")
            .WithOpenApi();
    }
}
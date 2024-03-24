using BoardGame.WarOfTheRing.Dice.Api.EndpointHandlers;

namespace BoardGame.WarOfTheRing.Dice.Api.EndpointMappings;

public static class DicePoolEndpointMappings
{
    public static void RegisterDicePoolEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var nationEndpoints = endpointRouteBuilder.MapGroup("/dicepool");

        nationEndpoints.MapPost("", DicePoolHandlers.RollDicePool)
            .WithName("RollDicePool")
            .WithOpenApi();
    }
}
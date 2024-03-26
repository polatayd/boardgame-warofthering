using BoardGame.WarOfTheRing.Fellowships.Api.EndpointHandlers;

namespace BoardGame.WarOfTheRing.Fellowships.Api.EndpointMappings;

public static class FellowshipEndpointMappings
{
    public static void RegisterFellowshipEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var nationEndpoints = endpointRouteBuilder.MapGroup("/fellowships");

        nationEndpoints.MapPost("", FellowshipHandlers.CreateFellowship)
            .WithName("CreateFellowship")
            .WithOpenApi();
    }
}
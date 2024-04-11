using BoardGame.WarOfTheRing.Maps.Api.EndpointHandlers;

namespace BoardGame.WarOfTheRing.Maps.Api.EndpointMappings;

public static class MapEndpointMappings
{
    public static void RegisterMapEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var mapsEndpoints = endpointRouteBuilder.MapGroup("/maps");

        mapsEndpoints.MapPost("", MapHandlers.CreateMap)
            .WithName("CreateMap")
            .WithOpenApi();
        
        mapsEndpoints.MapGet("", MapHandlers.GetMap)
            .WithName("GetMap")
            .WithOpenApi();
    }
}
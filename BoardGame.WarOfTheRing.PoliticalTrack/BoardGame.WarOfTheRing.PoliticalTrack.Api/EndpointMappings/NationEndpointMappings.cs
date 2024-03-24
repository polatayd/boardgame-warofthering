using BoardGame.WarOfTheRing.PoliticalTrack.Api.EndpointHandlers;

namespace BoardGame.WarOfTheRing.PoliticalTrack.Api.EndpointMappings;

public static class NationEndpointMappings
{
    public static void RegisterNationEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var nationEndpoints = endpointRouteBuilder.MapGroup("/nation");

        nationEndpoints.MapGet("", NationHandlers.GetNation)
            .WithName("GetNation")
            .WithOpenApi();
        nationEndpoints.MapPost("", NationHandlers.CreateNation)
            .WithName("CreateNation")
            .WithOpenApi();
        nationEndpoints.MapPut("/{name}/activation", NationHandlers.ActivateNation)
            .WithName("ActivateNation")
            .WithOpenApi();
        nationEndpoints.MapPost("/{name}/advancement", NationHandlers.AdvanceNation)
            .WithName("AdvanceNation")
            .WithOpenApi();
    }
}
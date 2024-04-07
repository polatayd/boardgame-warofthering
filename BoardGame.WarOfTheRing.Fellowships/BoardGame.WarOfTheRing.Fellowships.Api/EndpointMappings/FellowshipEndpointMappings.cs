using BoardGame.WarOfTheRing.Fellowships.Api.EndpointHandlers;

namespace BoardGame.WarOfTheRing.Fellowships.Api.EndpointMappings;

public static class FellowshipEndpointMappings
{
    public static void RegisterFellowshipEndpoints(this IEndpointRouteBuilder endpointRouteBuilder)
    {
        var fellowshipsEndpoints = endpointRouteBuilder.MapGroup("/fellowships");
        var fellowshipEndpoints = fellowshipsEndpoints.MapGroup("/{gameId:guid}");

        fellowshipsEndpoints.MapPost("", FellowshipHandlers.CreateFellowship)
            .WithName("CreateFellowship")
            .WithOpenApi();
        
        fellowshipEndpoints.MapPost("/forward", FellowshipHandlers.ForwardFellowship)
            .WithName("ForwardFellowship")
            .WithOpenApi();
        
        fellowshipEndpoints.MapGet("", FellowshipHandlers.GetFellowship)
            .WithName("GetFellowship")
            .WithOpenApi();
        
        fellowshipEndpoints.MapPost("/casualty", FellowshipHandlers.TakeCasualty)
            .WithName("TakeCasualty")
            .WithOpenApi();
        
        fellowshipEndpoints.MapPost("/reveal", FellowshipHandlers.Reveal)
            .WithName("Reveal")
            .WithOpenApi();
        
        fellowshipEndpoints.MapPost("/declare", FellowshipHandlers.Declare)
            .WithName("Declare")
            .WithOpenApi();
    }
}
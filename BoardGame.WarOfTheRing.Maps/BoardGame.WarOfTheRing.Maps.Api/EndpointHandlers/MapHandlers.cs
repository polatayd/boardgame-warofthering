using System.Text.Json;
using BoardGame.WarOfTheRing.Maps.Application.Maps.Commands;
using BoardGame.WarOfTheRing.Maps.Application.Maps.Inputs;
using BoardGame.WarOfTheRing.Maps.Application.Maps.Queries;
using BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BoardGame.WarOfTheRing.Maps.Api.EndpointHandlers;

public static class MapHandlers
{
    public static async Task<Results<ProblemHttpResult, Ok>> CreateMap([FromServices] IMediator mediator)
    {
        CreateNationsCommandInput createNationsCommandInput;
        using (var reader = new StreamReader("CreateConfig/nations.json"))
        {
            var nationsContent = await reader.ReadToEndAsync();
            createNationsCommandInput = JsonSerializer.Deserialize<CreateNationsCommandInput>(nationsContent);
        }

        CreateRegionsCommandInput createRegionsCommandInput;
        using (var reader = new StreamReader("CreateConfig/regions.json"))
        {
            var regionsContent = await reader.ReadToEndAsync();
            createRegionsCommandInput = JsonSerializer.Deserialize<CreateRegionsCommandInput>(regionsContent);
        }

        await mediator.Send(
            new CreateMapCommand(new CreateMapCommandInput(createNationsCommandInput, createRegionsCommandInput)));

        return TypedResults.Ok();
    }
    
    public static async Task<Results<ProblemHttpResult, Ok<Map>>> GetMap([FromServices] IMediator mediator)
    {
        var map = await mediator.Send(
            new GetMapQuery());

        return TypedResults.Ok(map);
    }
}
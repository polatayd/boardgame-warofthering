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
    public static async Task<CreateNationsCommandInput> GetCreateNationsCommandInputFromFile()
    {
        using var reader = new StreamReader("CreateConfig/nations.json");
        var nationsContent = await reader.ReadToEndAsync();
        return JsonSerializer.Deserialize<CreateNationsCommandInput>(nationsContent);
    }

    public static async Task<CreateRegionsCommandInput> GetCreateRegionsCommandInputFromFile()
    {
        using var reader = new StreamReader("CreateConfig/regions.json");
        var regionsContent = await reader.ReadToEndAsync();
        return JsonSerializer.Deserialize<CreateRegionsCommandInput>(regionsContent);
    }

    public static async Task<Results<ProblemHttpResult, Created>> CreateMap([FromServices] IMediator mediator)
    {
        await mediator.Send(new CreateMapCommand(new CreateMapCommandInput(
            await GetCreateNationsCommandInputFromFile(),
            await GetCreateRegionsCommandInputFromFile())));

        return TypedResults.Created();
    }

    public static async Task<Results<ProblemHttpResult, Ok<Map>>> GetMap([FromServices] IMediator mediator)
    {
        var map = await mediator.Send(new GetMapQuery());

        return TypedResults.Ok(map);
    }
}
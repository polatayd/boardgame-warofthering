using BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations.Commands;
using BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations.Exceptions;
using BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations.Inputs;
using BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations.Outputs;
using BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations.Queries;
using BoardGame.WarOfTheRing.PoliticalTrack.Domain.Aggregates.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BoardGame.WarOfTheRing.PoliticalTrack.Api.EndpointHandlers;

public static class NationHandlers
{
    public static async Task<Results<NotFound, Ok<GetNationRequestOutput>>> GetNation([FromQuery] string name,
        [FromQuery] Guid gameId,
        [FromServices] IMediator mediator)
    {
        var nation =
            await mediator.Send(new GetNationRequest(new GetNationRequestInput() { Name = name, GameId = gameId }));

        return nation is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(nation);
    }

    public static async Task<Results<ProblemHttpResult, Created>> CreateNation(
        [FromBody] CreateNationCommandInput createNationCommandInput,
        [FromServices] IMediator mediator)
    {
        try
        {
            await mediator.Send(new CreateNationCommand(createNationCommandInput));
        }
        catch (NationAlreadyExistException e)
        {
            return TypedResults.Problem(new ProblemDetails()
            {
                Status = StatusCodes.Status409Conflict,
                Type = nameof(NationAlreadyExistException),
                Title = "Nation Exception",
                Detail = e.Message,
            });
        }

        return TypedResults.Created();
    }

    public static async Task<Results<NotFound, Ok>> ActivateNation(
        [FromRoute] string name,
        [FromBody] ActivateNationInput activateNationInput,
        [FromServices] IMediator mediator)
    {
        try
        {
            await mediator.Send(new ActivateNationCommand(new ActivateNationCommandInput()
            {
                Name = name,
                GameId = activateNationInput.GameId
            }));
        }
        catch (NationNotFoundException)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok();
    }

    public static async Task<Results<NotFound, ProblemHttpResult, Ok>> AdvanceNation(
        [FromRoute] string name,
        [FromBody] AdvanceNationInput advanceNationInput,
        [FromServices] IMediator mediator,
        [FromServices] ILogger<AdvanceNationCommand> logger)
    {
        try
        {
            await mediator.Send(new AdvanceNationCommand(new AdvanceNationCommandInput()
            {
                Name = name,
                GameId = advanceNationInput.GameId
            }));
        }
        catch (NationNotFoundException)
        {
            return TypedResults.NotFound();
        }
        catch (PoliticalTrackAdvanceException e)
        {
            logger.LogError("GameId: {GameId}, Nation: {Nation}, Message: {Message}", advanceNationInput.GameId, name,
                e.Message);
            
            return TypedResults.Problem(new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Type = nameof(PoliticalTrackAdvanceException),
                Title = "Nation Exception",
                Detail = e.Message,
            });
        }

        return TypedResults.Ok();
    }
}

public class AdvanceNationInput
{
    public Guid GameId { get; set; }
}

public class ActivateNationInput
{
    public Guid GameId { get; set; }
}
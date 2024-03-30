using BoardGame.WarOfTheRing.Fellowships.Application.Hunts.Commands;
using BoardGame.WarOfTheRing.Fellowships.Application.Hunts.Exceptions;
using BoardGame.WarOfTheRing.Fellowships.Application.Hunts.Inputs;
using BoardGame.WarOfTheRing.Fellowships.Application.Hunts.Outputs;
using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts.Exceptions;
using BoardGame.WarOfTheRing.Fellowships.Infrastructure.Services.Dice;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BoardGame.WarOfTheRing.Fellowships.Api.EndpointHandlers;

public static class HuntHandlers
{
    public static async Task<Results<ProblemHttpResult, Ok<RollDiceCommandOutput>>> RollDice(
        [FromBody] RollDiceCommandInput rollDiceCommandInput,
        [FromServices] IMediator mediator)
    {
        RollDiceCommandOutput result;
        try
        {
            result = await mediator.Send(new RollDiceCommand(rollDiceCommandInput));
        }
        catch (HuntStateException e)
        {
            return TypedResults.Problem(new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Type = nameof(HuntStateException),
                Title = "Hunt Exception",
                Detail = e.Message,
            });
        }
        catch (HuntingNotFoundException e)
        {
            return TypedResults.Problem(new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Type = nameof(HuntingNotFoundException),
                Title = "Hunt Exception",
                Detail = e.Message,
            });
        }
        catch (DiceServiceException e)
        {
            return TypedResults.Problem(new ProblemDetails()
            {
                Status = StatusCodes.Status500InternalServerError,
                Type = nameof(DiceServiceException),
                Title = "Hunt Exception",
                Detail = e.Message,
            });
        }

        return TypedResults.Ok(result);
    }
}
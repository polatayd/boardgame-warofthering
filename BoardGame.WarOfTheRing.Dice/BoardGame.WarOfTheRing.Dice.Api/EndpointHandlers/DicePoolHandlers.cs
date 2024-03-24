using BoardGame.WarOfTheRing.Dice.Application.DicePools.Inputs;
using BoardGame.WarOfTheRing.Dice.Application.DicePools.Outputs;
using BoardGame.WarOfTheRing.Dice.Application.DicePools.Queries;
using BoardGame.WarOfTheRing.Dice.Domain.Aggregates;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BoardGame.WarOfTheRing.Dice.Api.EndpointHandlers;

public static class DicePoolHandlers
{
    public static async Task<Results<ProblemHttpResult, Ok<RollDicePoolOutput>>> RollDicePool(
        [FromBody] RollDicePoolInput rollDicePoolInput,
        [FromServices] IMediator mediator)
    {
        RollDicePoolOutput result;
        
        try
        {
            result = await mediator.Send(new RollDicePoolRequest(rollDicePoolInput));
        }
        catch (NumberOfDiceOutOfRangeException e)
        {
            return TypedResults.Problem(new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Type = nameof(NumberOfDiceOutOfRangeException),
                Title = "Dice Pool Exception",
                Detail = e.Message,
            });
        }
        
        return TypedResults.Ok(result);
    }
}
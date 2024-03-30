using BoardGame.WarOfTheRing.Dice.Application.DicePools.Inputs;
using BoardGame.WarOfTheRing.Dice.Application.DicePools.Outputs;
using BoardGame.WarOfTheRing.Dice.Application.DicePools.Queries;
using BoardGame.WarOfTheRing.Dice.Domain.Aggregates.DicePools.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BoardGame.WarOfTheRing.Dice.Api.EndpointHandlers;

public static class DicePoolHandlers
{
    public static async Task<Results<ValidationProblem, ProblemHttpResult, Ok<RollDicePoolOutput>>> RollDicePool(
        [FromBody] RollDicePoolInput rollDicePoolInput,
        [FromServices] IMediator mediator,
        [FromServices] IValidator<RollDicePoolInput> validator,
        [FromServices] ILogger<RollDicePoolRequest> logger)
    {
        var validationResult = await validator.ValidateAsync(rollDicePoolInput);
        
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(validationResult.ToDictionary());
        }
        
        RollDicePoolOutput result;

        try
        {
            result = await mediator.Send(new RollDicePoolRequest(rollDicePoolInput));
        }
        catch (NumberOfDiceOutOfRangeException e)
        {
            logger.LogError("DicePoolType: {DicePoolType}, NumberOfDice: {NumberOfDice}, Message: {Message}",
                rollDicePoolInput.DicePoolType, rollDicePoolInput.NumberOfDice,
                e.Message);

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
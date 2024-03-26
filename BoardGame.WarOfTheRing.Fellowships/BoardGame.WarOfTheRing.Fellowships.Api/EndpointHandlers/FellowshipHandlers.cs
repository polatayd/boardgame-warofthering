using BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.Commands;
using BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.Exceptions;
using BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.Inputs;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BoardGame.WarOfTheRing.Fellowships.Api.EndpointHandlers;

public static class FellowshipHandlers
{
    public static async Task<Results<ProblemHttpResult, Created>> CreateFellowship(
        [FromBody] CreateFellowshipCommandInput createFellowshipCommandInput,
        [FromServices] IMediator mediator)
    {
        try
        {
            await mediator.Send(new CreateFellowshipCommand(createFellowshipCommandInput));
        }
        catch (FellowshipAlreadyExistException e)
        {
            return TypedResults.Problem(new ProblemDetails()
            {
                Status = StatusCodes.Status409Conflict,
                Type = nameof(FellowshipAlreadyExistException),
                Title = "Fellowship Exception",
                Detail = e.Message,
            });
        }

        return TypedResults.Created();
    }
}
using BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.Commands;
using BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.Exceptions;
using BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.Inputs;
using BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.Outputs;
using BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.Queries;
using BoardGame.WarOfTheRing.Fellowships.Application.Hunts.Exceptions;
using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships.Exceptions;
using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts.Exceptions;
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
    
    public static async Task<Results<ProblemHttpResult, Ok>> ForwardFellowship(
        [FromRoute] Guid gameId,
        [FromServices] IMediator mediator)
    {
        try
        {
            await mediator.Send(new ForwardFellowshipCommand(new ForwardFellowshipCommandInput(gameId)));
        }
        catch (FellowshipNotFoundException e)
        {
            return TypedResults.Problem(new ProblemDetails()
            {
                Status = StatusCodes.Status404NotFound,
                Type = nameof(FellowshipNotFoundException),
                Title = "Fellowship Exception",
                Detail = e.Message,
            });
        }
        catch (FellowshipProgressCounterException e)
        {
            return TypedResults.Problem(new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Type = nameof(FellowshipProgressCounterException),
                Title = "Fellowship Exception",
                Detail = e.Message,
            });
        }

        return TypedResults.Ok();
    }
    
    public static async Task<Results<ProblemHttpResult, Ok<GetFellowshipQueryOutput>>> GetFellowship(
        [FromRoute] Guid gameId,
        [FromServices] IMediator mediator)
    {
        GetFellowshipQueryOutput fellowship;
        try
        {
            fellowship = await mediator.Send(new GetFellowshipQuery(new GetFellowshipQueryInput()
            {
                GameId = gameId
            }));
        }
        catch (FellowshipNotFoundException e)
        {
            return TypedResults.Problem(new ProblemDetails()
            {
                Status = StatusCodes.Status404NotFound,
                Type = nameof(FellowshipNotFoundException),
                Title = "Fellowship Exception",
                Detail = e.Message,
            });
        }

        return TypedResults.Ok(fellowship);
    }
    
    public static async Task<Results<ProblemHttpResult, Ok<TakeCasualtyCommandOutput>>> TakeCasualty(
        [FromRoute] Guid gameId,
        [FromBody] TakeCasualtyInput takeCasualtyInput,
        [FromServices] IMediator mediator)
    {
        TakeCasualtyCommandOutput output;
        try
        {
            output = await mediator.Send(new TakeCasualtyCommand(new TakeCasualtyCommandInput()
            {
                GameId = gameId,
                CasualtyType = takeCasualtyInput.CasualtyType
            }));
        }
        catch (FellowshipNotFoundException e)
        {
            return TypedResults.Problem(new ProblemDetails()
            {
                Status = StatusCodes.Status404NotFound,
                Type = nameof(FellowshipNotFoundException),
                Title = "Fellowship Exception",
                Detail = e.Message,
            });
        }
        catch (CharacterNotAvailableException e)
        {
            return TypedResults.Problem(new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Type = nameof(CharacterNotAvailableException),
                Title = "Fellowship Exception",
                Detail = e.Message,
            });
        }
        catch (HuntingNotFoundException e)
        {
            return TypedResults.Problem(new ProblemDetails()
            {
                Status = StatusCodes.Status404NotFound,
                Type = nameof(HuntingNotFoundException),
                Title = "Fellowship Exception",
                Detail = e.Message,
            });
        }
        catch (HuntStateException e)
        {
            return TypedResults.Problem(new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Type = nameof(HuntStateException),
                Title = "Fellowship Exception",
                Detail = e.Message,
            });
        }

        return TypedResults.Ok(output);
    }
    
    public static async Task<Results<ProblemHttpResult, Ok>> Reveal(
        [FromRoute] Guid gameId,
        [FromServices] IMediator mediator)
    {
        try
        {
            await mediator.Send(new RevealCommand(new RevealCommandInput()
            {
                GameId = gameId,
            }));
        }
        catch (FellowshipNotFoundException e)
        {
            return TypedResults.Problem(new ProblemDetails()
            {
                Status = StatusCodes.Status404NotFound,
                Type = nameof(FellowshipNotFoundException),
                Title = "Fellowship Exception",
                Detail = e.Message,
            });
        }
        catch (HuntingNotFoundException e)
        {
            return TypedResults.Problem(new ProblemDetails()
            {
                Status = StatusCodes.Status404NotFound,
                Type = nameof(HuntingNotFoundException),
                Title = "Fellowship Exception",
                Detail = e.Message,
            });
        }
        catch (HuntStateException e)
        {
            return TypedResults.Problem(new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Type = nameof(HuntStateException),
                Title = "Fellowship Exception",
                Detail = e.Message,
            });
        }

        return TypedResults.Ok();
    }
    
    public static async Task<Results<ProblemHttpResult, Ok>> Declare(
        [FromRoute] Guid gameId,
        [FromServices] IMediator mediator)
    {
        try
        {
            await mediator.Send(new DeclareCommand(new DeclareCommandInput()
            {
                GameId = gameId,
            }));
        }
        catch (FellowshipNotFoundException e)
        {
            return TypedResults.Problem(new ProblemDetails()
            {
                Status = StatusCodes.Status404NotFound,
                Type = nameof(FellowshipNotFoundException),
                Title = "Fellowship Exception",
                Detail = e.Message,
            });
        }
        catch (FellowshipDeclareException e)
        {
            return TypedResults.Problem(new ProblemDetails()
            {
                Status = StatusCodes.Status400BadRequest,
                Type = nameof(FellowshipDeclareException),
                Title = "Fellowship Exception",
                Detail = e.Message,
            });
        }

        return TypedResults.Ok();
    }
}

public class TakeCasualtyInput
{
    public CasualtyTypeInput CasualtyType { get; set; }
}
using BoardGame.WarOfTheRing.Fellowships.Application.Hunts.Exceptions;
using BoardGame.WarOfTheRing.Fellowships.Application.Hunts.Inputs;
using BoardGame.WarOfTheRing.Fellowships.Application.Hunts.Outputs;
using BoardGame.WarOfTheRing.Fellowships.Application.Services;
using MediatR;

namespace BoardGame.WarOfTheRing.Fellowships.Application.Hunts.Commands;

public class RollDiceCommand : IRequest<RollDiceCommandOutput>
{
    public RollDiceCommand(RollDiceCommandInput input)
    {
        Input = input;
    }

    public RollDiceCommandInput Input { get; set; }
}

public class RollDiceCommandHandler : IRequestHandler<RollDiceCommand, RollDiceCommandOutput>
{
    private readonly IDiceService diceService;
    private readonly IHuntRepository huntRepository;

    public RollDiceCommandHandler(IDiceService diceService, IHuntRepository huntRepository)
    {
        this.diceService = diceService;
        this.huntRepository = huntRepository;
    }

    public async Task<RollDiceCommandOutput> Handle(RollDiceCommand request, CancellationToken cancellationToken)
    {
        var hunting = huntRepository.GetByGameId(request.Input.GameId);

        if (hunting is null)
        {
            throw new HuntingNotFoundException("Hunting is not found");
        }

        var diceToRollCount = hunting.GetDiceToRollCount();
        if (diceToRollCount == 0)
        {
            return new RollDiceCommandOutput();
        }

        var diceResults = await diceService.SendRollDiceRequestAsync(diceToRollCount);
        
        //TODO: Send dice result to hunting. Update the success hunt dice results. Update the status for re-roll or other.

        return new RollDiceCommandOutput()
        {
            Results = diceResults
        };
    }
}
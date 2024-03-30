using BoardGame.WarOfTheRing.Fellowships.Application.Hunts.Exceptions;
using BoardGame.WarOfTheRing.Fellowships.Application.Hunts.Inputs;
using BoardGame.WarOfTheRing.Fellowships.Application.Hunts.Outputs;
using BoardGame.WarOfTheRing.Fellowships.Application.Services;
using MediatR;

namespace BoardGame.WarOfTheRing.Fellowships.Application.Hunts.Commands;

//TODO: Think seperate the RollDiceCommand and ReRollDiceCommand in domain layer logics.
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
    private readonly IUnitOfWork unitOfWork;

    public RollDiceCommandHandler(IDiceService diceService, IHuntRepository huntRepository, IUnitOfWork unitOfWork)
    {
        this.diceService = diceService;
        this.huntRepository = huntRepository;
        this.unitOfWork = unitOfWork;
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
        hunting.EvaluateRollResult(diceResults);

        bool rerollIsAvailable = false;
        if (hunting.IsAvailableForReRollCalculation())
        {
            //TODO: Fetch fellowship and send it to the map service to get if reroll is available or not.
            rerollIsAvailable = true;
        }

        hunting.EvaluateNextHuntMove(rerollIsAvailable);

        await unitOfWork.SaveChangesAsync();

        return new RollDiceCommandOutput()
        {
            Results = diceResults
        };
    }
}
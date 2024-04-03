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
    private readonly IMapService mapService;
    private readonly IHuntRepository huntRepository;
    private readonly IUnitOfWork unitOfWork;

    public RollDiceCommandHandler(IDiceService diceService, IHuntRepository huntRepository, IMapService mapService,
        IUnitOfWork unitOfWork)
    {
        this.diceService = diceService;
        this.huntRepository = huntRepository;
        this.mapService = mapService;
        this.unitOfWork = unitOfWork;
    }

    public async Task<RollDiceCommandOutput> Handle(RollDiceCommand request, CancellationToken cancellationToken)
    {
        var hunting = huntRepository.GetByGameId(request.Input.GameId);
        if (hunting is null)
        {
            throw new HuntingNotFoundException("Hunting is not found");
        }

        var diceToRollCount = hunting.GetDiceToRollCountForRoll();
        if (diceToRollCount == 0)
        {
            return new RollDiceCommandOutput();
        }
            
        var availableReRollCount = await mapService.SendReRollCountRequestAsync(hunting.GameId);
        var diceResults = await diceService.SendRollDiceRequestAsync(diceToRollCount);
        
        hunting.CalculateSuccessRollsForRollDice(diceResults, availableReRollCount);

        await unitOfWork.SaveChangesAsync();

        return new RollDiceCommandOutput()
        {
            Results = diceResults
        };
    }
}
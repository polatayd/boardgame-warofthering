using BoardGame.WarOfTheRing.Fellowships.Application.Hunts.Exceptions;
using BoardGame.WarOfTheRing.Fellowships.Application.Hunts.Inputs;
using BoardGame.WarOfTheRing.Fellowships.Application.Hunts.Outputs;
using BoardGame.WarOfTheRing.Fellowships.Application.Services;
using MediatR;

namespace BoardGame.WarOfTheRing.Fellowships.Application.Hunts.Commands;

public class ReRollDiceCommand : IRequest<RollDiceCommandOutput>
{
    public ReRollDiceCommand(RollDiceCommandInput input)
    {
        Input = input;
    }

    public RollDiceCommandInput Input { get; set; }
}

public class ReRollDiceCommandHandler : IRequestHandler<ReRollDiceCommand, RollDiceCommandOutput>
{
    private readonly IDiceService diceService;
    private readonly IHuntRepository huntRepository;
    private readonly IUnitOfWork unitOfWork;

    public ReRollDiceCommandHandler(IDiceService diceService, IHuntRepository huntRepository,
        IUnitOfWork unitOfWork)
    {
        this.diceService = diceService;
        this.huntRepository = huntRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<RollDiceCommandOutput> Handle(ReRollDiceCommand request, CancellationToken cancellationToken)
    {
        var hunting = huntRepository.GetByGameId(request.Input.GameId);
        if (hunting is null)
        {
            throw new HuntingNotFoundException("Hunting is not found");
        }

        var diceToRollCount = hunting.GetDiceToRollCountForReRoll();
        if (diceToRollCount == 0)
        {
            return new RollDiceCommandOutput();
        }

        var diceResults = await diceService.SendRollDiceRequestAsync(diceToRollCount);
        hunting.CalculateSuccessRollsForReRollDice(diceResults);

        await unitOfWork.SaveChangesAsync();

        return new RollDiceCommandOutput()
        {
            Results = diceResults
        };
    }
}
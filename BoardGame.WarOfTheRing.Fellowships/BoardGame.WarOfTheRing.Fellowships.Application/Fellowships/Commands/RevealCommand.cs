using BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.Exceptions;
using BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.Inputs;
using BoardGame.WarOfTheRing.Fellowships.Application.Hunts;
using BoardGame.WarOfTheRing.Fellowships.Application.Hunts.Exceptions;
using BoardGame.WarOfTheRing.Fellowships.Application.Services;
using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts.Exceptions;
using MediatR;

namespace BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.Commands;

public class RevealCommand(RevealCommandInput revealCommandInput) : IRequest
{
    public RevealCommandInput Input { get; } = revealCommandInput;
}

public class RevealCommandHandler : IRequestHandler<RevealCommand>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IFellowshipRepository fellowshipRepository;
    private readonly IHuntRepository huntRepository;
    private readonly IMapService mapService;

    public RevealCommandHandler(IUnitOfWork unitOfWork, IFellowshipRepository fellowshipRepository, IHuntRepository huntRepository, IMapService mapService)
    {
        this.unitOfWork = unitOfWork;
        this.fellowshipRepository = fellowshipRepository;
        this.huntRepository = huntRepository;
        this.mapService = mapService;
    }

    public async Task Handle(RevealCommand request, CancellationToken cancellationToken)
    {
        var fellowship = fellowshipRepository.Get(request.Input.GameId);
        if (fellowship is null)
        {
            throw new FellowshipNotFoundException();
        }

        var hunting = huntRepository.GetById(fellowship.HuntingId);
        if (hunting is null)
        {
            throw new HuntingNotFoundException("Hunting is not found");
        }

        if (!hunting.ActiveHunt.IsInRevealState())
        {
            throw new HuntStateException("Can not be revealed");
        }
        
        await mapService.SendMoveFellowshipRequestAsync(fellowship.GameId);

        fellowship.Reveal();

        await unitOfWork.SaveChangesAsync();
    }
}
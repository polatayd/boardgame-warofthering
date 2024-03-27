using BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.Exceptions;
using BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.Inputs;
using BoardGame.WarOfTheRing.Fellowships.Application.Hunts;
using MediatR;

namespace BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.Commands;

public class ForwardFellowshipCommand(ForwardFellowshipCommandInput forwardFellowshipCommandInput) : IRequest
{
    public ForwardFellowshipCommandInput Input { get; } = forwardFellowshipCommandInput;
}

public class ForwardFellowshipCommandHandler : IRequestHandler<ForwardFellowshipCommand>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IFellowshipRepository fellowshipRepository;
    private readonly IHuntRepository huntRepository;

    public ForwardFellowshipCommandHandler(IUnitOfWork unitOfWork, IFellowshipRepository fellowshipRepository, IHuntRepository huntRepository)
    {
        this.unitOfWork = unitOfWork;
        this.fellowshipRepository = fellowshipRepository;
        this.huntRepository = huntRepository;
    }

    public async Task Handle(ForwardFellowshipCommand request, CancellationToken cancellationToken)
    {
        var fellowship = fellowshipRepository.Get(request.Input.GameId);
        if (fellowship is null)
            throw new FellowshipNotFoundException();
        var hunting = huntRepository.GetById(fellowship.HuntingId);
        
        fellowship.ForwardProgressCounter(hunting.ActiveHunt.State);

        await unitOfWork.SaveChangesAsync();
    }
}
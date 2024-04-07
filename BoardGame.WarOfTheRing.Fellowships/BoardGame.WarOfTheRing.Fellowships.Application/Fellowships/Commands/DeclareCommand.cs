using BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.Exceptions;
using BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.Inputs;
using BoardGame.WarOfTheRing.Fellowships.Application.Services;
using MediatR;

namespace BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.Commands;

public class DeclareCommand(DeclareCommandInput declareCommandInput) : IRequest
{
    public DeclareCommandInput Input { get; } = declareCommandInput;
}

public class DeclareCommandHandler : IRequestHandler<DeclareCommand>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IFellowshipRepository fellowshipRepository;
    private readonly IMapService mapService;

    public DeclareCommandHandler(IUnitOfWork unitOfWork, IFellowshipRepository fellowshipRepository, IMapService mapService)
    {
        this.unitOfWork = unitOfWork;
        this.fellowshipRepository = fellowshipRepository;
        this.mapService = mapService;
    }

    public async Task Handle(DeclareCommand request, CancellationToken cancellationToken)
    {
        var fellowship = fellowshipRepository.Get(request.Input.GameId);
        if (fellowship is null)
        {
            throw new FellowshipNotFoundException();
        }

        var declaredRegion = await mapService.SendMoveFellowshipRequestAsync(fellowship.GameId);

        fellowship.Declare(declaredRegion);

        await unitOfWork.SaveChangesAsync();
    }
}
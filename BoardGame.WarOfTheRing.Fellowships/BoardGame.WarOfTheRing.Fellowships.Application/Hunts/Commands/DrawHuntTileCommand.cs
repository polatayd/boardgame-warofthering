using BoardGame.WarOfTheRing.Fellowships.Application.Hunts.Exceptions;
using BoardGame.WarOfTheRing.Fellowships.Application.Hunts.Inputs;
using BoardGame.WarOfTheRing.Fellowships.Application.Hunts.Outputs;
using MediatR;

namespace BoardGame.WarOfTheRing.Fellowships.Application.Hunts.Commands;

public class DrawHuntTileCommand : IRequest<DrawHuntTileCommandOutput>
{
    public DrawHuntTileCommand(DrawHuntTileCommandInput input)
    {
        Input = input;
    }

    public DrawHuntTileCommandInput Input { get; set; }
}

public class DrawHuntTileCommandHandler : IRequestHandler<DrawHuntTileCommand, DrawHuntTileCommandOutput>
{
    private readonly IHuntRepository huntRepository;
    private readonly IUnitOfWork unitOfWork;

    public DrawHuntTileCommandHandler(IHuntRepository huntRepository, IUnitOfWork unitOfWork)
    {
        this.huntRepository = huntRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task<DrawHuntTileCommandOutput> Handle(DrawHuntTileCommand request, CancellationToken cancellationToken)
    {
        var hunting = huntRepository.GetByGameId(request.Input.GameId);
        if (hunting is null)
        {
            throw new HuntingNotFoundException("Hunting is not found");
        }

        var drawnHuntTile = hunting.DrawHuntTile();

        await unitOfWork.SaveChangesAsync();

        return new DrawHuntTileCommandOutput
        {
            HuntDamage = drawnHuntTile.HuntDamage,
            HasRevealIcon = drawnHuntTile.HasRevealIcon,
            HasStopIcon = drawnHuntTile.HasStopIcon,
            HasEyeIcon = drawnHuntTile.HasEyeIcon,
            HasDieIcon = drawnHuntTile.HasDieIcon
        };
    }
}
using BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.Exceptions;
using BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.Inputs;
using BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.Outputs;
using BoardGame.WarOfTheRing.Fellowships.Application.Hunts;
using BoardGame.WarOfTheRing.Fellowships.Application.Hunts.Exceptions;
using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts.Exceptions;
using MediatR;

namespace BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.Commands;

public class TakeCasualtyCommand(TakeCasualtyCommandInput takeCasualtyCommandInput) : IRequest<TakeCasualtyCommandOutput>
{
    public TakeCasualtyCommandInput Input { get; } = takeCasualtyCommandInput;
}

public class TakeCasualtyCommandHandler : IRequestHandler<TakeCasualtyCommand, TakeCasualtyCommandOutput>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IFellowshipRepository fellowshipRepository;
    private readonly IHuntRepository huntRepository;

    public TakeCasualtyCommandHandler(IUnitOfWork unitOfWork, IFellowshipRepository fellowshipRepository, IHuntRepository huntRepository)
    {
        this.unitOfWork = unitOfWork;
        this.fellowshipRepository = fellowshipRepository;
        this.huntRepository = huntRepository;
    }

    public async Task<TakeCasualtyCommandOutput> Handle(TakeCasualtyCommand request, CancellationToken cancellationToken)
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

        if (!hunting.ActiveHunt.IsInTakeCasualtyState())
        {
            throw new HuntStateException("Casualty can not be taken");
        }

        var damage = hunting.GetDamage();

        var casualtyCharacter = request.Input.CasualtyType switch
        {
            CasualtyTypeInput.None => fellowship.TakeNoneCasualty(damage),
            CasualtyTypeInput.Guide => fellowship.TakeGuideCasualty(damage),
            CasualtyTypeInput.Random => fellowship.TakeRandomCasualty(damage),
            _ => throw new ArgumentOutOfRangeException()
        };

        await unitOfWork.SaveChangesAsync();

        if (casualtyCharacter is null)
        {
            return new TakeCasualtyCommandOutput()
            {
                IsCasualtyTaken = false,
                Casualty = null
            };
        }

        return new TakeCasualtyCommandOutput()
        {
            IsCasualtyTaken = true,
            Casualty = new CharacterOutput()
            {
                Level = casualtyCharacter.Level,
                Name = casualtyCharacter.Name.ToString()
            }
        };
    }
}
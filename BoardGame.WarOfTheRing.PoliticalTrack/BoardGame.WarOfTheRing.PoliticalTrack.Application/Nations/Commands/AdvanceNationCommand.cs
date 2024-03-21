using BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations.Exceptions;
using BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations.Factories;
using BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations.Inputs;
using BoardGame.WarOfTheRing.PoliticalTrack.Domain.Aggregates;
using BoardGame.WarOfTheRing.PoliticalTrack.Domain.ValueObjects;
using MediatR;

namespace BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations.Commands;

public class AdvanceNationCommand(AdvanceNationCommandInput input) : IRequest
{
    public AdvanceNationCommandInput Input { get; } = input;
}

public class AdvanceNationCommandHandler : IRequestHandler<AdvanceNationCommand>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly INationRepository nationRepository;

    public AdvanceNationCommandHandler(IUnitOfWork unitOfWork, INationRepository nationRepository)
    {
        this.unitOfWork = unitOfWork;
        this.nationRepository = nationRepository;
    }

    public async Task Handle(AdvanceNationCommand request, CancellationToken cancellationToken)
    {
        var nation = nationRepository.Get(request.Input.Name);

        if (nation == null)
        {
            throw new NationNotFoundException();
        }
        
        nation.AdvanceOnPoliticalTrack();

        await unitOfWork.SaveChangesAsync();
    }
}
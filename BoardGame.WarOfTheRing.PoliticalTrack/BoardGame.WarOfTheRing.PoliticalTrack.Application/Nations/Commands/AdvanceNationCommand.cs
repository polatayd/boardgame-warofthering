using BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations.Exceptions;
using BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations.Inputs;
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
        var nation = nationRepository.Get(request.Input.Name, request.Input.GameId);

        if (nation == null)
        {
            throw new NationNotFoundException();
        }
        
        nation.AdvanceOnPoliticalTrack();

        await unitOfWork.SaveChangesAsync();
    }
}
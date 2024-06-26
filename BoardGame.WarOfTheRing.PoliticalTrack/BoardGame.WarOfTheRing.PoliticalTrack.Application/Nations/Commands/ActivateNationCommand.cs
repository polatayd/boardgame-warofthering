using BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations.Exceptions;
using BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations.Inputs;
using MediatR;

namespace BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations.Commands;

public class ActivateNationCommand(ActivateNationCommandInput input) : IRequest
{
    public ActivateNationCommandInput Input { get; } = input;
}

public class ActivateNationCommandHandler : IRequestHandler<ActivateNationCommand>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly INationRepository nationRepository;

    public ActivateNationCommandHandler(IUnitOfWork unitOfWork, INationRepository nationRepository)
    {
        this.unitOfWork = unitOfWork;
        this.nationRepository = nationRepository;
    }

    public async Task Handle(ActivateNationCommand request, CancellationToken cancellationToken)
    {
        var nation = nationRepository.Get(request.Input.Name, request.Input.GameId);

        if (nation == null)
        {
            throw new NationNotFoundException();
        }
        
        nation.Activate();

        await unitOfWork.SaveChangesAsync();
    }
}
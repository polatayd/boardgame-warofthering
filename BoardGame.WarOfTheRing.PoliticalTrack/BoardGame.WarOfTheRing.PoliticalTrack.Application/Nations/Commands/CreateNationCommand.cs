using BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations.Exceptions;
using BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations.Factories;
using BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations.Inputs;
using BoardGame.WarOfTheRing.PoliticalTrack.Domain.Aggregates;
using BoardGame.WarOfTheRing.PoliticalTrack.Domain.ValueObjects;
using MediatR;

namespace BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations.Commands;

public class CreateNationCommand(CreateNationCommandInput input) : IRequest
{
    public CreateNationCommandInput Input { get; } = input;
}

public class CreateNationCommandHandler : IRequestHandler<CreateNationCommand>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly INationRepository nationRepository;

    public CreateNationCommandHandler(IUnitOfWork unitOfWork, INationRepository nationRepository)
    {
        this.unitOfWork = unitOfWork;
        this.nationRepository = nationRepository;
    }

    public async Task Handle(CreateNationCommand request, CancellationToken cancellationToken)
    {
        var existingNation = nationRepository.Get(request.Input.Name);

        if (existingNation is not null)
        {
            throw new NationAlreadyExistException();
        }
        
        var status = NationFactory.CreateStatus(request.Input.Status);
        var position = new Position(request.Input.Position);
        var name = new Name(request.Input.Name);

        var nation = new Nation(status, position, name);

        nationRepository.Add(nation);

        await unitOfWork.SaveChangesAsync();
    }
}
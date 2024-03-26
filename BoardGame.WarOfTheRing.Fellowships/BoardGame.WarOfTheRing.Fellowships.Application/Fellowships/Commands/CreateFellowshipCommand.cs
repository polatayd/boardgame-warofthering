using BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.Exceptions;
using BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.Inputs;
using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships;
using MediatR;

namespace BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.Commands;

public class CreateFellowshipCommand(CreateFellowshipCommandInput createFellowshipCommandInput) : IRequest
{
    public CreateFellowshipCommandInput Input { get; } = createFellowshipCommandInput;
}

public class CreateFellowshipCommandHandler : IRequestHandler<CreateFellowshipCommand>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IFellowshipRepository fellowshipRepository;

    public CreateFellowshipCommandHandler(IUnitOfWork unitOfWork, IFellowshipRepository fellowshipRepository)
    {
        this.unitOfWork = unitOfWork;
        this.fellowshipRepository = fellowshipRepository;
    }

    public async Task Handle(CreateFellowshipCommand request, CancellationToken cancellationToken)
    {
        var existingFellowship = fellowshipRepository.Get(request.Input.GameId);

        if (existingFellowship is not null)
        {
            throw new FellowshipAlreadyExistException();
        }
        
        var fellowship = Fellowship.Create(request.Input.GameId);

        fellowshipRepository.Add(fellowship);
        await unitOfWork.SaveChangesAsync();
    }
}
using BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.Exceptions;
using BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.Inputs;
using BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.IntegrationEvents;
using BoardGame.WarOfTheRing.Fellowships.Application.OutboxMessages;
using BoardGame.WarOfTheRing.Fellowships.Application.Services;
using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships.DomainEvents;
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
    private readonly IOutboxMessageRepository outboxMessageRepository;
    private readonly IMapService mapService;

    public DeclareCommandHandler(IUnitOfWork unitOfWork, IFellowshipRepository fellowshipRepository,
        IMapService mapService, IOutboxMessageRepository outboxMessageRepository)
    {
        this.unitOfWork = unitOfWork;
        this.fellowshipRepository = fellowshipRepository;
        this.mapService = mapService;
        this.outboxMessageRepository = outboxMessageRepository;
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

        var outboxMessage = fellowship.DomainEvents.Select(source =>
        {
            if (source is FellowshipDeclared fellowshipDeclaredEvent)
            {
                return new FellowshipDeclaredIntegrationEvent(fellowshipDeclaredEvent.GameId,
                    fellowshipDeclaredEvent.NationName).ToOutboxMessage();
            }

            return null;
        }).FirstOrDefault();

        if (outboxMessage is not null)
        {
            outboxMessageRepository.Add(outboxMessage);
        }

        await unitOfWork.SaveChangesAsync();
    }
}
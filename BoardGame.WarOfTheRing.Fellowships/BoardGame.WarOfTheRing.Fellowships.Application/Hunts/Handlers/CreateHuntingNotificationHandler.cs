using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships.DomainEvents;
using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts;
using MediatR;

namespace BoardGame.WarOfTheRing.Fellowships.Application.Hunts.Handlers;

public class CreateHuntingNotificationHandler : INotificationHandler<DomainEventNotification<FellowshipCreated>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IHuntRepository huntRepository;

    public CreateHuntingNotificationHandler(IUnitOfWork unitOfWork, IHuntRepository huntRepository)
    {
        this.unitOfWork = unitOfWork;
        this.huntRepository = huntRepository;
    }
    
    public async Task Handle(DomainEventNotification<FellowshipCreated> notification, CancellationToken cancellationToken)
    {
        var fellowshipCreated = notification.DomainEvent;

        var hunting = new Hunting(fellowshipCreated.HuntingId, fellowshipCreated.FellowshipId,
            fellowshipCreated.GameId);
        
        huntRepository.Add(hunting);
        await unitOfWork.SaveChangesAsync();
    }
}
using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships.DomainEvents;
using MediatR;

namespace BoardGame.WarOfTheRing.Fellowships.Application.Hunts.Handlers;

public class FellowshipRevealedNotificationHandler : INotificationHandler<DomainEventNotification<FellowshipRevealed>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IHuntRepository huntRepository;

    public FellowshipRevealedNotificationHandler(IUnitOfWork unitOfWork, IHuntRepository huntRepository)
    {
        this.unitOfWork = unitOfWork;
        this.huntRepository = huntRepository;
    }
    
    public async Task Handle(DomainEventNotification<FellowshipRevealed> notification, CancellationToken cancellationToken)
    {
        var fellowshipRevealed = notification.DomainEvent;

        var hunting = huntRepository.GetById(fellowshipRevealed.HuntingId);

        hunting.CompleteReveal();

        await unitOfWork.SaveChangesAsync();
    }
}
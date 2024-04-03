using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships.DomainEvents;
using MediatR;

namespace BoardGame.WarOfTheRing.Fellowships.Application.Hunts.Handlers;

public class TakeCasualtyDamageNotificationHandler : INotificationHandler<DomainEventNotification<CasualtyTaken>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IHuntRepository huntRepository;

    public TakeCasualtyDamageNotificationHandler(IUnitOfWork unitOfWork, IHuntRepository huntRepository)
    {
        this.unitOfWork = unitOfWork;
        this.huntRepository = huntRepository;
    }
    
    public async Task Handle(DomainEventNotification<CasualtyTaken> notification, CancellationToken cancellationToken)
    {
        var casualtyTaken = notification.DomainEvent;

        var hunting = huntRepository.GetById(casualtyTaken.HuntingId);

        hunting.CompleteTakeCasualty();

        await unitOfWork.SaveChangesAsync();
    }
}
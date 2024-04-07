using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships.DomainEvents;
using MediatR;

namespace BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.Handlers;

public class FellowshipDeclaredInCityOrStrongholdNotificationHandler : INotificationHandler<DomainEventNotification<FellowshipDeclaredInCityOrStronghold>>
{
    public Task Handle(DomainEventNotification<FellowshipDeclaredInCityOrStronghold> notification, CancellationToken cancellationToken)
    {
        var fellowshipDeclared = notification.DomainEvent;
        
        //TODO: Send integration event to rabbitmq.
        
        return Task.CompletedTask;
    }
}
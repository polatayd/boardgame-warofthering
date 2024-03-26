using BoardGame.WarOfTheRing.Fellowships.Domain.Base;
using MediatR;

namespace BoardGame.WarOfTheRing.Fellowships.Application;

public class DomainEventNotification<TDomainEvent> : INotification where TDomainEvent : DomainEvent
{
    public TDomainEvent DomainEvent { get; }

    public DomainEventNotification(TDomainEvent domainEvent)
    {
        if(EqualityComparer<TDomainEvent>.Default.Equals(domainEvent, default))
        {
            throw new ArgumentNullException(nameof(domainEvent));
        }

        DomainEvent = domainEvent;
    }
}
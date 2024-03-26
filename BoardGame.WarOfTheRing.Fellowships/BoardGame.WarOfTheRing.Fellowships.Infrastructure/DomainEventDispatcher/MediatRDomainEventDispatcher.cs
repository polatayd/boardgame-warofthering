using BoardGame.WarOfTheRing.Fellowships.Application;
using BoardGame.WarOfTheRing.Fellowships.Domain.Base;
using MediatR;

namespace BoardGame.WarOfTheRing.Fellowships.Infrastructure.DomainEventDispatcher;

public class MediatRDomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IMediator mediator;

    public MediatRDomainEventDispatcher(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public async Task Dispatch(IEnumerable<EntityBase> entitiesWithEvents)
    {
        foreach (var entity in entitiesWithEvents)
        {
            var events = entity.DomainEvents.ToArray();
            entity.ClearDomainEvents();
            foreach (var domainEvent in events)
            {
                var domainEventNotification = CreateDomainEventNotification((dynamic)domainEvent);
                
                await mediator.Publish(domainEventNotification).ConfigureAwait(false);
            }
        }
    }
    
    private static DomainEventNotification<TDomainEvent> CreateDomainEventNotification<TDomainEvent>(TDomainEvent domainEvent) 
        where TDomainEvent : DomainEvent
    {
        return new DomainEventNotification<TDomainEvent>(domainEvent);
    }
}


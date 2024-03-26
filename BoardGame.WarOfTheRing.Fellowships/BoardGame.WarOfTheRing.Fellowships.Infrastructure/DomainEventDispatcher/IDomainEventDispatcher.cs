using BoardGame.WarOfTheRing.Fellowships.Domain.Base;

namespace BoardGame.WarOfTheRing.Fellowships.Infrastructure.DomainEventDispatcher;

public interface IDomainEventDispatcher
{
    Task Dispatch(IEnumerable<EntityBase> entitiesWithEvents);
}
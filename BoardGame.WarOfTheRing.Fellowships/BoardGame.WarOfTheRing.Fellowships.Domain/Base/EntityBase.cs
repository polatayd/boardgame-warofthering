using System.ComponentModel.DataAnnotations.Schema;

namespace BoardGame.WarOfTheRing.Fellowships.Domain.Base;

public abstract class EntityBase
{
    public Guid Id { get; init; }
    
    private List<DomainEvent> domainEvents = new();
    [NotMapped] public IReadOnlyList<DomainEvent> DomainEvents => domainEvents.AsReadOnly();

    protected void RegisterDomainEvent(DomainEvent domainEvent) => domainEvents.Add(domainEvent);
    public void ClearDomainEvents() => domainEvents.Clear();
}
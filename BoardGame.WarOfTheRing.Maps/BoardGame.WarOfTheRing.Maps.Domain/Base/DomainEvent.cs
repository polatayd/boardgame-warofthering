namespace BoardGame.WarOfTheRing.Maps.Domain.Base;

public abstract class DomainEvent
{
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
}
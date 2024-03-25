namespace BoardGame.WarOfTheRing.Fellowship.Domain.Base;

public abstract class DomainEvent
{
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
}
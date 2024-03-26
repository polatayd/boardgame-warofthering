namespace BoardGame.WarOfTheRing.Fellowships.Domain.Base;

public abstract class DomainEvent
{
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
}
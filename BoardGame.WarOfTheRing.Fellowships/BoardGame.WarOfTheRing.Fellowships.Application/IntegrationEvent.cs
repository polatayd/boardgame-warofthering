namespace BoardGame.WarOfTheRing.Fellowships.Application;

public abstract class IntegrationEvent
{
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
}
using BoardGame.WarOfTheRing.Fellowships.Domain.Base;

namespace BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships.DomainEvents;

public class FellowshipDeclaredInCityOrStronghold : DomainEvent
{
    public Guid GameId { get; }

    public FellowshipDeclaredInCityOrStronghold(Guid gameId)
    {
        GameId = gameId;
    }
}
using BoardGame.WarOfTheRing.Fellowships.Domain.Base;

namespace BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships.DomainEvents;

public class FellowshipDeclared : DomainEvent
{
    public Guid GameId { get; }
    public string NationName { get; set; }

    public FellowshipDeclared(Guid gameId, string nationName)
    {
        GameId = gameId;
        NationName = nationName;
    }
}
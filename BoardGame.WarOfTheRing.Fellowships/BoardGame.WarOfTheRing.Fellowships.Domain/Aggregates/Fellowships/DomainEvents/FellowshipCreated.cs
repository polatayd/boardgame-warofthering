using BoardGame.WarOfTheRing.Fellowships.Domain.Base;

namespace BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships.DomainEvents;

public class FellowshipCreated : DomainEvent
{
    public Guid FellowshipId { get; init; }
    public Guid HuntingId { get; init; }
    public Guid GameId { get; init; }

    public FellowshipCreated(Guid fellowshipId, Guid huntingId, Guid gameId)
    {
        FellowshipId = fellowshipId;
        HuntingId = huntingId;
        GameId = gameId;
    }
}
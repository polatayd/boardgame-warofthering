using BoardGame.WarOfTheRing.Fellowship.Domain.Base;

namespace BoardGame.WarOfTheRing.Fellowship.Domain.Aggregates.Fellowships.DomainEvents;

public class FellowshipCreated : DomainEvent
{
    public Guid FellowshipId { get; init; }
    public Guid FellowshipHuntingId { get; init; }

    public FellowshipCreated(Guid fellowshipId, Guid fellowshipHuntingId)
    {
        FellowshipId = fellowshipId;
        FellowshipHuntingId = fellowshipHuntingId;
    }
}
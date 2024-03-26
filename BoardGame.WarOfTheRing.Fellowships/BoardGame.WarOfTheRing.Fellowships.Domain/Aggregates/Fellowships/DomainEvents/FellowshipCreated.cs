using BoardGame.WarOfTheRing.Fellowships.Domain.Base;

namespace BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships.DomainEvents;

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
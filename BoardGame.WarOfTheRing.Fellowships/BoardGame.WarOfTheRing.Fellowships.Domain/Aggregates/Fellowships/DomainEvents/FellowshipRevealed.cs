using BoardGame.WarOfTheRing.Fellowships.Domain.Base;

namespace BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships.DomainEvents;

public class FellowshipRevealed : DomainEvent
{
    public Guid HuntingId { get; }

    public FellowshipRevealed(Guid huntingId)
    {
        HuntingId = huntingId;
    }
}
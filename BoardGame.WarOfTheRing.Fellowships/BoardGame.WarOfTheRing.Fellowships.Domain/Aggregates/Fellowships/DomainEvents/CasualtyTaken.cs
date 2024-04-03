using BoardGame.WarOfTheRing.Fellowships.Domain.Base;

namespace BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships.DomainEvents;

public class CasualtyTaken : DomainEvent
{
    public Guid HuntingId { get; }

    public CasualtyTaken(Guid huntingId)
    {
        HuntingId = huntingId;
    }
}
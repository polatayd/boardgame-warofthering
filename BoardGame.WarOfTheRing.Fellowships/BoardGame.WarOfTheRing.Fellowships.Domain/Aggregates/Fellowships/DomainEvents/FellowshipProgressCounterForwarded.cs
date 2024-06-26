using BoardGame.WarOfTheRing.Fellowships.Domain.Base;

namespace BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships.DomainEvents;

public class FellowshipProgressCounterForwarded : DomainEvent
{
    public Guid FellowshipId { get; init; }
    public Guid HuntingId { get; init; }
    public int ProgressCounterValue { get; init; }
    
    public FellowshipProgressCounterForwarded(Guid fellowshipId, Guid huntingId, int progressCounterValue)
    {
        FellowshipId = fellowshipId;
        HuntingId = huntingId;
        ProgressCounterValue = progressCounterValue;
    }
}
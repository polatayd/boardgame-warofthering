using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts.Exceptions;
using BoardGame.WarOfTheRing.Fellowships.Domain.Base;

namespace BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts.ValueObjects;

public class Hunt : ValueObject
{
    public Hunt()
    {
        State = HuntState.Empty;
    }

    private Hunt(HuntState huntState)
    {
        State = huntState;
    }

    public HuntState State { get; init; }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return State;
    }

    public Hunt Start()
    {
        if (State != HuntState.Empty)
        {
            throw new HuntStateException("Hunt can not be started if it's already active");
        }
        
        return new Hunt(HuntState.RollDiceNeeded);
    }
}
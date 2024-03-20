using BoardGame.WarOfTheRing.PoliticalTrack.Domain.Aggregates.Exceptions;
using BoardGame.WarOfTheRing.PoliticalTrack.Domain.Base;

namespace BoardGame.WarOfTheRing.PoliticalTrack.Domain.ValueObjects;

public class Track : ValueObject
{
    private readonly int position;

    public Track(int position)
    {
        if (position < 0 || position > 3)
        {
            throw new ArgumentOutOfRangeException();
        }
        
        this.position = position;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return position;
    }

    public bool IsInAtWarPosition()
    {
        return position == 3;
    }
    
    public bool IsInOneStepBehindAtWarPosition()
    {
        return position == 2;
    }

    public Track AdvancePosition()
    {
        if (IsInAtWarPosition())
        {
            throw new PoliticalTrackAdvanceException(PoliticalTrackAdvanceException.Reason.BecauseOfOutOfRange);
        }
       
        return new Track(position + 1);
    }
}
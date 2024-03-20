namespace BoardGame.WarOfTheRing.PoliticalTrack.Domain.Aggregates.Exceptions;

public class PoliticalTrackAdvanceException(PoliticalTrackAdvanceException.Reason reason) : Exception
{
    public enum Reason
    {
        BecauseOfPassiveStatus,
        BecauseOfOutOfRange
    }

    public override string ToString()
    {
        return reason switch
        {
            Reason.BecauseOfPassiveStatus => "Passive Nation can not advance At War Position",
            Reason.BecauseOfOutOfRange => "Nation can not advance no more",
            _ => base.ToString()
        };
    }
}
namespace BoardGame.WarOfTheRing.PoliticalTrack.Domain.Aggregates.Exceptions;

public class PoliticalTrackAdvanceException : ApplicationException
{
    public enum Reason
    {
        BecauseOfPassiveStatus,
        BecauseOfOutOfRange
    }

    public PoliticalTrackAdvanceException(Reason reason)
    {
        Message = GetMessage(reason);
    }

    public override string Message { get; }

    private string GetMessage(Reason reason)
    {
        return reason switch
        {
            Reason.BecauseOfPassiveStatus => "Passive Nation can not advance At War Position",
            Reason.BecauseOfOutOfRange => "Nation can not advance no more",
            _ => base.ToString()
        };
    }
}
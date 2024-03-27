namespace BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships.Exceptions;

public class FellowshipProgressCounterException : ApplicationException
{
    public FellowshipProgressCounterException(string message) : base(message)
    {
    }
}
namespace BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts.Exceptions;

public class HuntStateException : ApplicationException
{
    public HuntStateException(string message) : base(message)
    {
    }
}
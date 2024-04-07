namespace BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships.Exceptions;

public class FellowshipDeclareException : ApplicationException
{
    public FellowshipDeclareException(string message) : base(message)
    {
    }
}
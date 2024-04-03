namespace BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships.Exceptions;

public class CharacterNotAvailableException : ApplicationException
{
    public CharacterNotAvailableException(string message) : base(message)
    {
    }
}
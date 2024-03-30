namespace BoardGame.WarOfTheRing.Fellowships.Infrastructure.Services.Dice;

public class DiceServiceException : ApplicationException
{
    public DiceServiceException(string message) : base(message)
    {
    }
}
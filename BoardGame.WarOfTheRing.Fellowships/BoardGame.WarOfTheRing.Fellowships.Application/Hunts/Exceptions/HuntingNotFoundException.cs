namespace BoardGame.WarOfTheRing.Fellowships.Application.Hunts.Exceptions;

public class HuntingNotFoundException : ApplicationException
{
    public HuntingNotFoundException(string message) : base(message)
    {
    }
}
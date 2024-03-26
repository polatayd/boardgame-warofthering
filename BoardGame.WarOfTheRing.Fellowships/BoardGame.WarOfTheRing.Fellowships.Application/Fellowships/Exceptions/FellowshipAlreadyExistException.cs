namespace BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.Exceptions;

public class FellowshipAlreadyExistException : ApplicationException
{
    public FellowshipAlreadyExistException() : base("Fellowship is already exist")
    {
    }
}
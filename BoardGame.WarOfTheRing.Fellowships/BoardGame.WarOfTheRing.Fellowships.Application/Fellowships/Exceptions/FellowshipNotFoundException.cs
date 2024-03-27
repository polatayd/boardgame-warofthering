namespace BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.Exceptions;

public class FellowshipNotFoundException : ApplicationException
{
    public FellowshipNotFoundException() : base("Fellowship is not found")
    {
    }
}
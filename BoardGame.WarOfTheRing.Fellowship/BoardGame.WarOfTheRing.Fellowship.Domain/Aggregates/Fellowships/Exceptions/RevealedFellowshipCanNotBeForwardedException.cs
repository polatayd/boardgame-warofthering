namespace BoardGame.WarOfTheRing.Fellowship.Domain.Aggregates.Fellowships.Exceptions;

public class RevealedFellowshipCanNotBeForwardedException : ApplicationException
{
    public RevealedFellowshipCanNotBeForwardedException() : base("Revealed Fellowship can not be forwarded")
    {
    }
}
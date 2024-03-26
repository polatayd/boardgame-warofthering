namespace BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts.Exceptions;

public class HuntTileCreationError : ApplicationException
{
    public HuntTileCreationError(string message) : base(message)
    {
    }
}
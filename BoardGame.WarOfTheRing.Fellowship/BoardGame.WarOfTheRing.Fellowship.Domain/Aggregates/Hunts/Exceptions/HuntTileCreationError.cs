namespace BoardGame.WarOfTheRing.Fellowship.Domain.Aggregates.Hunts.Exceptions;

public class HuntTileCreationError : ApplicationException
{
    public HuntTileCreationError(string message) : base(message)
    {
    }
}
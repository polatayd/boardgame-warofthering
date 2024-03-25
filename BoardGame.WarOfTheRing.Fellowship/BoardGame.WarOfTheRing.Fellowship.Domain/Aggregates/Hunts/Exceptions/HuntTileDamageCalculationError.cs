namespace BoardGame.WarOfTheRing.Fellowship.Domain.Aggregates.Hunts.Exceptions;

public class HuntTileDamageCalculationError : ApplicationException
{
    public HuntTileDamageCalculationError(string message) : base(message)
    {
    }
}
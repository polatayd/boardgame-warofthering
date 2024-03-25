namespace BoardGame.WarOfTheRing.Dice.Domain.Aggregates.DicePools.Exceptions;

public class NumberOfDiceOutOfRangeException : ApplicationException
{
    public override string Message => "Number of dice can not be more than five, if it's numbered dice.";
}
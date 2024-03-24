using BoardGame.WarOfTheRing.Dice.Domain.ValueObjects;

namespace BoardGame.WarOfTheRing.Dice.Domain.Aggregates;

public static class DicePoolFactory
{
    public static List<Die> CreateDicePool(DicePoolType dicePoolType, ushort numberOfDice)
    {
        var dice = new List<Die>();

        Func<Die> addDieMethod;
        switch (dicePoolType)
        {
            case DicePoolType.FreePeoplesActionDicePool:
                addDieMethod = FreePeoplesActionDie.Create;
                break;
            case DicePoolType.ShadowPlayerActionDicePool:
                addDieMethod = ShadowActionDie.Create;
                break;
            case DicePoolType.NumberedDicePool:
                if (numberOfDice > 5)
                {
                    throw new NumberOfDiceOutOfRangeException();
                }

                addDieMethod = NumberedDie.Create;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(dicePoolType), dicePoolType, null);
        }

        for (var i = 0; i < numberOfDice; i++)
        {
            dice.Add(addDieMethod());
        }

        return dice;
    }
}
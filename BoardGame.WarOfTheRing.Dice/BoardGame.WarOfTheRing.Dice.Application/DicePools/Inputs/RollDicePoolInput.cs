namespace BoardGame.WarOfTheRing.Dice.Application.DicePools.Inputs;

public class RollDicePoolInput
{
    public ushort NumberOfDice { get; set; }
    public DicePoolTypeInput DicePoolType { get; set; }
}

public enum DicePoolTypeInput
{
    FreePeoplesActionDicePool,
    ShadowPlayerActionDicePool,
    NumberedDicePool
}
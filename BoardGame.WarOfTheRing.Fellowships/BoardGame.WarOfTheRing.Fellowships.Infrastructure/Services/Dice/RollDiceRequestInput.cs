namespace BoardGame.WarOfTheRing.Fellowships.Infrastructure.Services.Dice;

public class RollDiceRequestInput
{
    public int NumberOfDice { get; set; }
    public string DicePoolType { get; } = "NumberedDicePool";
}
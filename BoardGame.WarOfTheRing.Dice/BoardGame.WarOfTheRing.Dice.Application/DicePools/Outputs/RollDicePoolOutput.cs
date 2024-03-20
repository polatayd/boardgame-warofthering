namespace BoardGame.WarOfTheRing.Dice.Application.DicePools.Outputs;

public class RollDicePoolOutput
{
    public List<DieFaceResult> Results { get; set; }
}

public class DieFaceResult
{
    public string Name { get; set; }
    public ushort Value { get; set; }
}
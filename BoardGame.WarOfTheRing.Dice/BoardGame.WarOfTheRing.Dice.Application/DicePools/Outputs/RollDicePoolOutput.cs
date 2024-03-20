namespace BoardGame.WarOfTheRing.Dice.Application.DicePools.Outputs;

public class RollDicePoolOutput(List<DieFaceResult> results)
{
    public List<DieFaceResult> Results { get; } = results;
}

public class DieFaceResult
{
    public string? Name { get; set; }
    public ushort Value { get; set; }
}
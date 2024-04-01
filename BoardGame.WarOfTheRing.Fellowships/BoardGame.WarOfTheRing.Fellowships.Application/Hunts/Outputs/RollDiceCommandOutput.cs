namespace BoardGame.WarOfTheRing.Fellowships.Application.Hunts.Outputs;

public class RollDiceCommandOutput
{
    public IReadOnlyList<int> Results { get; set; } = new List<int>();
}
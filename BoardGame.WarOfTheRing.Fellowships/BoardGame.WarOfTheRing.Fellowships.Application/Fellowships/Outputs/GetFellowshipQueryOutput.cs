namespace BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.Outputs;

public class GetFellowshipQueryOutput
{
    public int ProgressCounterValue { get; set; }
    public int CorruptionCounterValue { get; set; }
    public List<CharacterOutput> Characters { get; set; } = new();
    public CharacterOutput Guide { get; set; }
}
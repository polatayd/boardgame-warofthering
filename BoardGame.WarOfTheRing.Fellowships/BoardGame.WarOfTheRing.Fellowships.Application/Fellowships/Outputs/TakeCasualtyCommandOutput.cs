namespace BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.Outputs;

public class TakeCasualtyCommandOutput
{
    public CharacterOutput Casualty { get; set; }
    public bool IsCasualtyTaken { get; set; }
}
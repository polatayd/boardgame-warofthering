namespace BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.Inputs;

public class TakeCasualtyCommandInput
{
    public Guid GameId { get; set; }
    public CasualtyTypeInput CasualtyType { get; set; }
}

public enum CasualtyTypeInput
{
    None,
    Guide,
    Random
}
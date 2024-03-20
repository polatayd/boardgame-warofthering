namespace BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations.Outputs;

public class GetNationRequestOutput
{
    public StatusOutput Status { get; set; }
    public int Position { get; set; }
    public string Name { get; set; }
}

public enum StatusOutput
{
    Active,
    Passive
}
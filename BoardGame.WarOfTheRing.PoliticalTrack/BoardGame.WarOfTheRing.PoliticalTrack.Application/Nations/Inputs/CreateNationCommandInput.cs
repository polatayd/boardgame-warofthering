namespace BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations.Inputs;

public class CreateNationCommandInput
{
    public StatusInput Status { get; set; }
    public int Position { get; set; }
    public string Name { get; set; }
    public Guid GameId { get; set; }
}

public enum StatusInput
{
    Active,
    Passive
}
namespace BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations.Inputs;

public class ActivateNationCommandInput
{
    public string Name { get; set; }
    public Guid GameId { get; set; }
}
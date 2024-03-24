namespace BoardGame.WarOfTheRing.PoliticalTrack.Application.Nations.Inputs;

public class GetNationRequestInput
{
    public string Name { get; set; }
    public Guid GameId { get; set; }
}
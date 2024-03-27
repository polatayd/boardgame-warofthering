namespace BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.Inputs;

public class ForwardFellowshipCommandInput
{
    public ForwardFellowshipCommandInput(Guid gameId)
    {
        GameId = gameId;
    }

    public Guid GameId { get; set; }
}
namespace BoardGame.WarOfTheRing.Fellowships.Infrastructure.Services.Dice;

public class RollDiceRequestOutput
{
    public List<RequestResult> Results { get; set; }
}

public class RequestResult
{
    public int Value { get; set; }
}
namespace BoardGame.WarOfTheRing.Fellowships.Api.ServiceRegistrations;

public class ServicesOptions
{
    public const string Services = "Services";
    
    public DiceOptions Dice { get; set; }
}

public class DiceOptions
{
    public string BaseAddress { get; set; }
    public int Timeout { get; set; }
}
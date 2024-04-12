namespace BoardGame.WarOfTheRing.Maps.Application.Maps.Inputs;

public class CreateNationsCommandInput
{
    public List<CreateNationInput> Nations { get; set; }
}

public class CreateNationInput
{
    public string Name { get; set; }
    public string BelongsTo { get; set; }
    public CreateReinforcementsInput Reinforcements { get; set; }
}

public class CreateReinforcementsInput
{
    public int RegularCount { get; set; }
    public int EliteCount { get; set; }
    public int LeaderCount { get; set; }
}
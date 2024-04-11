namespace BoardGame.WarOfTheRing.Maps.Application.Maps.Inputs;

public class CreateRegionsCommandInput
{
    public List<CreateRegionInput> Regions { get; set; }
}

public class CreateRegionInput
{
    public string Name { get; set; }
    public string Terrain { get; set; }
    public string ControlledBy { get; set; } = null;
    public string InBorderOf { get; set; }
    public CreateArmyInput Army { get; set; }
    public List<string> NeighborRegions { get; set; }
}

public class CreateArmyInput
{
    public int RegularCount { get; set; }
    public int EliteCount { get; set; }
}
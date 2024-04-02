namespace BoardGame.WarOfTheRing.Fellowships.Application.Hunts.Outputs;

public class DrawHuntTileCommandOutput
{
    public int HuntDamage { get; set; }
    public bool HasRevealIcon { get; set; }
    public bool HasStopIcon { get; set; }
    public bool HasEyeIcon { get; set; }
    public bool HasDieIcon { get; set; }
}
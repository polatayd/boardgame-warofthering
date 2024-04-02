using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts.Exceptions;
using BoardGame.WarOfTheRing.Fellowships.Domain.Base;

namespace BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts.ValueObjects;

public class HuntTile : ValueObject
{
    public int HuntDamage { get; private set; }
    public bool HasRevealIcon { get; private set; }
    public bool HasStopIcon { get; private set; }
    public bool HasEyeIcon { get; private set; }
    public bool HasDieIcon { get; private set; }

    private HuntTile(int huntDamage, bool hasEyeIcon, bool hasDieIcon, bool hasRevealIcon, bool hasStopIcon)
    {
        if (hasRevealIcon && hasStopIcon)
        {
            throw new HuntTileCreationError("A Tile can not have both reveal and stop icon");
        }

        HuntDamage = huntDamage;
        HasEyeIcon = hasEyeIcon;
        HasDieIcon = hasDieIcon;
        HasRevealIcon = hasRevealIcon;
        HasStopIcon = hasStopIcon;
    }

    internal static HuntTile CreateFromHuntTile(HuntTile huntTile)
    {
        return new HuntTile(huntTile.HuntDamage, huntTile.HasEyeIcon, huntTile.HasDieIcon, huntTile.HasRevealIcon,
            huntTile.HasStopIcon);
    }

    public static HuntTile CreateNumberedTile(int huntDamage)
    {
        return new HuntTile(huntDamage, false, false, false, false);
    }
    
    public static HuntTile CreateEyeTile()
    {
        return new HuntTile(0, true, false, false, false);
    }
    
    public static HuntTile CreateDieTile()
    {
        return new HuntTile(0, false, true, false, false);
    }

    public HuntTile WithRevealIcon()
    {
        return new HuntTile(HuntDamage, HasEyeIcon, HasDieIcon, true, HasStopIcon);
    }
    
    public HuntTile WithStopIcon()
    {
        return new HuntTile(HuntDamage, HasEyeIcon, HasDieIcon, HasRevealIcon, true);
    }

    public int GetDamage()
    {
        if (HasEyeIcon || HasDieIcon)
        {
            throw new HuntTileDamageCalculationError("Eye or Die damage must be calculated");
        }

        return HuntDamage;
    }

    public int GetEyeDamage(int numberOfSuccessRolls)
    {
        if (!HasEyeIcon)
        {
            throw new HuntTileDamageCalculationError("Eye damage can not be calculated without Eye icon");
        }

        return numberOfSuccessRolls;
    }

    public int GetDieDamage(int resultOfRoll)
    {
        if (!HasDieIcon)
        {
            throw new HuntTileDamageCalculationError("Die damage can not be calculated without Die icon");
        }

        return resultOfRoll;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return HuntDamage;
        yield return HasRevealIcon;
        yield return HasStopIcon;
        yield return HasEyeIcon;
        yield return HasDieIcon;
    }
}
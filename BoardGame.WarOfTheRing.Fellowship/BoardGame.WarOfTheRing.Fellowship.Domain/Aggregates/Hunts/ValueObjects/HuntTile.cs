using BoardGame.WarOfTheRing.Fellowship.Domain.Aggregates.Hunts.Exceptions;
using BoardGame.WarOfTheRing.Fellowship.Domain.Base;

namespace BoardGame.WarOfTheRing.Fellowship.Domain.Aggregates.Hunts.ValueObjects;

public class HuntTile : ValueObject
{
    public int HuntDamage { get; private set; }
    public bool HasRevealIcon { get; private set; }
    public bool HasStopIcon { get; private set; }
    public bool HasEyeIcon { get; private set; }
    public bool HasDieIcon { get; private set; }

    public HuntTile(int huntDamage, bool hasRevealIcon, bool hasStopIcon, bool hasEyeIcon, bool hasDieIcon)
    {
        if (huntDamage != 0)
        {
            if (hasEyeIcon || hasDieIcon)
            {
                throw new HuntTileCreationError("A Tile can not have eye or die icon if it has damage");
            }
        }
        else
        {
            if (!hasEyeIcon && !hasDieIcon)
            {
                throw new HuntTileCreationError("A Tile must have eye or die icon if it has no damage");
            }
        }
        
        if (hasRevealIcon && hasStopIcon)
        {
            throw new HuntTileCreationError("A Tile can not have both reveal and stop icon");
        }

        if (hasEyeIcon && hasDieIcon)
        {
            throw new HuntTileCreationError("A Tile can not have both eye and die icon");
        }

        HuntDamage = huntDamage;
        HasRevealIcon = hasRevealIcon;
        HasStopIcon = hasStopIcon;
        HasEyeIcon = hasEyeIcon;
        HasDieIcon = hasDieIcon;
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
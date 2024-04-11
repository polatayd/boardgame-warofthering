using BoardGame.WarOfTheRing.Maps.Domain.Base;

namespace BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps.ValueObjects;

public class UnitType : ValueObject
{
    private UnitType() {}
    private UnitType(string value)
    {
        Value = value;
    }

    public static UnitType Regular()
    {
        return new UnitType(UnitTypes.Regular);
    }
    
    public static UnitType Elite()
    {
        return new UnitType(UnitTypes.Elite);
    }

    public static UnitType FromValue(string value)
    {
        if (value == UnitTypes.Regular)
        {
            return Regular();
        }

        if (value == UnitTypes.Elite)
        {
            return Elite();
        }

        throw new ArgumentException();
    }

    public string Value { get; init; }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}

public static class UnitTypes
{
    public static string Regular => nameof(Regular);
    public static string Elite => nameof(Elite);
}
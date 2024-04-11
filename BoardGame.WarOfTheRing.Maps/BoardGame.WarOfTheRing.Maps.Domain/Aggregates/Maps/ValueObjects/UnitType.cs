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
        return new UnitType("Regular");
    }
    
    public static UnitType Elite()
    {
        return new UnitType("Elite");
    }

    public static UnitType FromValue(string value)
    {
        if (value == "Regular")
        {
            return Regular();
        }

        if (value == "Elite")
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
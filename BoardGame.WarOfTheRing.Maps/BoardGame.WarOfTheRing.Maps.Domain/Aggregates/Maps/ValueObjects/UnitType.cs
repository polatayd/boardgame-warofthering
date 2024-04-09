using BoardGame.WarOfTheRing.Maps.Domain.Base;

namespace BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps.ValueObjects;

public class UnitType : ValueObject
{
    public static UnitType Regular { get; } = new UnitType(nameof(Regular));
    public static UnitType Elite { get; } = new UnitType(nameof(Elite));
    
    private UnitType(string value)
    {
        Value = value;
    }

    public string Value { get; init; }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
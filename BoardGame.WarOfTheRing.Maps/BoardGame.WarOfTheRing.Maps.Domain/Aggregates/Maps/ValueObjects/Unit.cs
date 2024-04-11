using BoardGame.WarOfTheRing.Maps.Domain.Base;

namespace BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps.ValueObjects;

public class Unit : ValueObject
{
    private Unit() {}
    private Unit(string nationName, UnitType type)
    {
        NationName = nationName;
        Type = type;
    }

    public string NationName { get; init; }
    public UnitType Type { get; init; }

    public static Unit Create(string nationName, UnitType type)
    {
        return new Unit(nationName, type);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return NationName;
        yield return Type;
    }
}
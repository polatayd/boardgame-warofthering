using BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps.Entities;
using BoardGame.WarOfTheRing.Maps.Domain.Base;

namespace BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps.ValueObjects;

public class Unit : ValueObject
{
    public Unit(string nationName, UnitType type)
    {
        NationName = nationName;
        Type = type;
    }

    public string NationName { get; init; }
    public UnitType Type { get; init; }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return NationName;
        yield return Type;
    }
}
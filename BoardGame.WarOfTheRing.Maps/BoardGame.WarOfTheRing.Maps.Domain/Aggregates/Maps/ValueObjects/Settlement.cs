using BoardGame.WarOfTheRing.Maps.Domain.Base;

namespace BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps.ValueObjects;

public class Settlement : ValueObject
{
    private Settlement() {}
    private Settlement(string type, int victoryPoint, Force controlledBy)
    {
        Type = type;
        VictoryPoint = victoryPoint;
        ControlledBy = controlledBy;
    }

    public string Type { get; init; }
    public int VictoryPoint { get; init; }
    public Force ControlledBy { get; init; }
    
    public static Settlement None()
    {
        return new Settlement(SettlementTypes.None, 0, Force.None());
    }

    public static Settlement Town(Force controlledBy)
    {
        return new Settlement(SettlementTypes.Town, 0, controlledBy);
    }
    
    public static Settlement City(Force controlledBy)
    {
        return new Settlement(SettlementTypes.City, 1, controlledBy);
    }
    
    public static Settlement Stronghold(Force controlledBy)
    {
        return new Settlement(SettlementTypes.Stronghold, 2, controlledBy);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Type;
        yield return VictoryPoint;
        yield return ControlledBy;
    }
}

public static class SettlementTypes
{
    public static string None => nameof(None);
    public static string City => nameof(City);
    public static string Town => nameof(Town);
    public static string Stronghold => nameof(Stronghold);
}
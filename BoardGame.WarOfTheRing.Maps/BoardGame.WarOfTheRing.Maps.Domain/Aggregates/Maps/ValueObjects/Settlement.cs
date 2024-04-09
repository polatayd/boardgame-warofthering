using BoardGame.WarOfTheRing.Maps.Domain.Base;

namespace BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps.ValueObjects;

public class Settlement : ValueObject
{
    public static Settlement None { get; } = new Settlement(nameof(None), 0, Force.None);
    
    private Settlement(string type, int victoryPoint, Force controlledBy)
    {
        Type = type;
        VictoryPoint = victoryPoint;
        ControlledBy = controlledBy;
    }

    public string Type { get; init; }
    public int VictoryPoint { get; init; }
    public Force ControlledBy { get; init; }

    public static Settlement Town(Force controlledBy)
    {
        return new Settlement("Town", 0, controlledBy);
    }
    
    public static Settlement City(Force controlledBy)
    {
        return new Settlement("City", 1, controlledBy);
    }
    
    public static Settlement Stronghold(Force controlledBy)
    {
        return new Settlement("Stronghold", 2, controlledBy);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Type;
        yield return VictoryPoint;
        yield return ControlledBy;
    }
}
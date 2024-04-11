using BoardGame.WarOfTheRing.Maps.Domain.Base;

namespace BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps.ValueObjects;

public class Terrain : ValueObject
{
    private Terrain() {}
    private Terrain(bool isEmpty, bool hasFortification, Settlement settlement)
    {
        IsEmpty = isEmpty;
        HasFortification = hasFortification;
        Settlement = settlement;
    }

    public bool IsEmpty { get; init; }
    public bool HasFortification { get; init; }
    public Settlement Settlement { get; init; }

    public static Terrain CreateEmpty()
    {
        return new Terrain(true, false, Settlement.None());
    }
    
    public static Terrain CreateFortification()
    {
        return new Terrain(false, true, Settlement.None());
    }
    
    public static Terrain CreateSettlement(Settlement settlement)
    {
        return new Terrain(false, false, settlement);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return IsEmpty;
        yield return HasFortification;
        yield return Settlement;
    }
}

public static class TerrainTypes
{
    public static string Empty => nameof(Empty);
    public static string Fortification => nameof(Fortification);
}
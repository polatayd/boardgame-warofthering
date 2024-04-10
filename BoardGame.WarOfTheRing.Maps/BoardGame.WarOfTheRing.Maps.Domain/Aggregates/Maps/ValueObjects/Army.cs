using BoardGame.WarOfTheRing.Maps.Domain.Base;

namespace BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps.ValueObjects;

public class Army : ValueObject
{
    private List<Unit> units = new();
    public IReadOnlyList<Unit> Units => units.AsReadOnly();

    private Army() {}
    private Army(List<Unit> units)
    {
        this.units = units;
    }

    public static Army Create()
    {
        return new Army();
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        return Units;
    }
}
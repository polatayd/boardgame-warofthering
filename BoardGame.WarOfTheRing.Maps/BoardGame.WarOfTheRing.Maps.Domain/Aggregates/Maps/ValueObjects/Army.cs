using BoardGame.WarOfTheRing.Maps.Domain.Base;

namespace BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps.ValueObjects;

public class Army : ValueObject
{
    public static Army Empty { get; } = new Army();
    
    private List<Unit> units = new();
    public IReadOnlyList<Unit> Units => units.AsReadOnly();
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        return Units;
    }
}
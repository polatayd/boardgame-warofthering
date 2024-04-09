using BoardGame.WarOfTheRing.Maps.Domain.Base;

namespace BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps.ValueObjects;

public class Force : ValueObject
{
    public static Force None { get; } = new Force(nameof(None));
    public static Force Shadow { get; } = new Force(nameof(Shadow));
    public static Force FreePeoples { get; } = new Force(nameof(FreePeoples));
    
    private Force(string name)
    {
        Name = name;
    }

    public string Name { get; init; }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
    }
}
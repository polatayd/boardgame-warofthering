using BoardGame.WarOfTheRing.Maps.Domain.Base;

namespace BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps.ValueObjects;

public class Force : ValueObject
{
    private Force() {}
    private Force(string name)
    {
        Name = name;
    }

    public static Force None()
    {
        return new Force("None");
    }
    
    public static Force Shadow()
    {
        return new Force("Shadow");
    }
    
    public static Force FreePeoples()
    {
        return new Force("FreePeoples");
    }

    public string Name { get; init; }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
    }
}
using BoardGame.WarOfTheRing.Maps.Domain.Base;

namespace BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps.ValueObjects;

public class Neighbor : ValueObject
{
    public Neighbor(string name)
    {
        Name = name;
    }

    public string Name { get; init; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
    }
}
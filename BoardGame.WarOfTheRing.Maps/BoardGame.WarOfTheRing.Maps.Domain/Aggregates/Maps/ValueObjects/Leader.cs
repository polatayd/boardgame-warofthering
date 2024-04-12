using BoardGame.WarOfTheRing.Maps.Domain.Base;

namespace BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps.ValueObjects;

public class Leader : ValueObject
{
    protected Leader() {}
    protected Leader(string nationName, bool isNazgul)
    {
        NationName = nationName;
        IsNazgul = isNazgul;
    }

    public string NationName { get; init; }
    public bool IsNazgul { get; init; }

    public static Leader Create(string nationName)
    {
        return new Leader(nationName, false);
    }

    public Leader WithNazgul()
    {
        return new Leader(NationName, true);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return NationName;
        yield return IsNazgul;
    }
}
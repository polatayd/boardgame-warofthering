using BoardGame.WarOfTheRing.Fellowships.Domain.Base;

namespace BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships.ValueObjects;

public class Region : ValueObject
{
    public Region(RegionPlayer ownedBy, RegionPlayer controlledBy, RegionType type, string ownedNationName)
    {
        OwnedBy = ownedBy;
        ControlledBy = controlledBy;
        Type = type;
        OwnedNationName = ownedNationName;
    }

    public RegionPlayer OwnedBy { get; init; }
    public RegionPlayer ControlledBy { get; init; }
    public RegionType Type { get; init; }
    public string OwnedNationName { get; init; }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return OwnedBy;
        yield return ControlledBy;
        yield return Type;
        yield return OwnedNationName;
    }
}

public enum RegionPlayer
{
    None,
    FreePeoples,
    Shadow
}

public enum RegionType
{
    None,
    City,
    Stronghold,
    Fortification
}
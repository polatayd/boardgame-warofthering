using BoardGame.WarOfTheRing.PoliticalTrack.Domain.Base;

namespace BoardGame.WarOfTheRing.PoliticalTrack.Domain.ValueObjects;

public class Status(bool isActive) : ValueObject
{
    public static readonly Status Active = new Status(true);
    public static readonly Status Passive = new Status(false);

    public bool IsActive { get; } = isActive;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return IsActive;
    }
}
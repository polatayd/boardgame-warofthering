using System.Diagnostics.CodeAnalysis;
using BoardGame.WarOfTheRing.PoliticalTrack.Domain.Base;

namespace BoardGame.WarOfTheRing.PoliticalTrack.Domain.Aggregates.Nations.ValueObjects;

public class Status : ValueObject
{
    public static readonly Status Active = new Status(true);
    public static readonly Status Passive = new Status(false);

    public bool IsActive { get; init; }

    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    private Status() {}

    public Status(bool isActive)
    {
        IsActive = isActive;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return IsActive;
    }
}
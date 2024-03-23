using System.Diagnostics.CodeAnalysis;
using BoardGame.WarOfTheRing.PoliticalTrack.Domain.Base;

namespace BoardGame.WarOfTheRing.PoliticalTrack.Domain.ValueObjects;

public class Name : ValueObject
{
    public string Value { get; init; }

    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    private Name() {}

    public Name(string value)
    {
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
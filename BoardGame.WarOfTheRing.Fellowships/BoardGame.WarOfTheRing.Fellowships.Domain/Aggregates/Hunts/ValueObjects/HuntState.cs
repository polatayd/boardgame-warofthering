using System.Diagnostics.CodeAnalysis;
using BoardGame.WarOfTheRing.Fellowships.Domain.Base;

namespace BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts.ValueObjects;

public class HuntState : ValueObject
{
    public static HuntState Empty { get; } = new("Empty");
    public static HuntState RollDiceNeeded { get; } = new("RollDiceNeeded");
    
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    private HuntState() {}
    private HuntState(string value)
    {
        Value = value;
    }

    public string Value { get; init; }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
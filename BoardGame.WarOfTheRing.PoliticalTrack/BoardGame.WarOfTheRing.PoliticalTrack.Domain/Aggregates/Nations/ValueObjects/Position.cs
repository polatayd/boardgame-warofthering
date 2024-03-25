using System.Diagnostics.CodeAnalysis;
using BoardGame.WarOfTheRing.PoliticalTrack.Domain.Aggregates.Nations.Exceptions;
using BoardGame.WarOfTheRing.PoliticalTrack.Domain.Base;

namespace BoardGame.WarOfTheRing.PoliticalTrack.Domain.Aggregates.Nations.ValueObjects;

public class Position : ValueObject
{
    public int Value { get; init; }
    
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    private Position() {}

    public Position(int value)
    {
        if (value < 0 || value > 3)
        {
            throw new ArgumentOutOfRangeException();
        }
        
        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public bool IsInAtWarPosition()
    {
        return Value == 3;
    }
    
    public bool IsInOneStepBehindAtWarPosition()
    {
        return Value == 2;
    }

    public Position AdvancePosition()
    {
        if (IsInAtWarPosition())
        {
            throw new PoliticalTrackAdvanceException(PoliticalTrackAdvanceException.Reason.BecauseOfOutOfRange);
        }
       
        return new Position(Value + 1);
    }
}
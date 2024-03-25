using BoardGame.WarOfTheRing.Fellowship.Domain.Aggregates.Fellowships.Exceptions;
using BoardGame.WarOfTheRing.Fellowship.Domain.Base;

namespace BoardGame.WarOfTheRing.Fellowship.Domain.Aggregates.Fellowships.ValueObjects;

public class ProgressCounter : ValueObject
{
    public int Value { get; init; }
    public bool IsHidden { get; init; }
    
    public ProgressCounter()
    {
        Value = 0;
        IsHidden = true;
    }
    
    private ProgressCounter(int value, bool isHidden)
    {
        if (value < 0 || value > 12)
        {
            throw new ArgumentOutOfRangeException();
        }
        
        Value = value;
        IsHidden = isHidden;
    }

    public ProgressCounter Forward()
    {
        if (!IsHidden)
        {
            throw new RevealedFellowshipCanNotBeForwardedException();
        }
        
        return new ProgressCounter(Value + 1, IsHidden);
    }
    
    public ProgressCounter MoveToZero()
    {
        return new ProgressCounter(0, IsHidden);
    }

    public ProgressCounter Hide()
    {
        return new ProgressCounter(Value, true);
    }
    
    public ProgressCounter Reveal()
    {
        return new ProgressCounter(Value, false);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return IsHidden;
    }
}
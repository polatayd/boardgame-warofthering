using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships.Exceptions;
using BoardGame.WarOfTheRing.Fellowships.Domain.Base;

namespace BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships.ValueObjects;

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
            throw new FellowshipProgressCounterException("Revealed Fellowship can not be forwarded");
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
        return new ProgressCounter(0, false);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return IsHidden;
    }
}
using BoardGame.WarOfTheRing.Fellowship.Domain.Base;

namespace BoardGame.WarOfTheRing.Fellowship.Domain.Aggregates.Fellowships.ValueObjects;

public class CorruptionCounter : ValueObject
{
    public int Value { get; init; }
    
    public CorruptionCounter()
    {
        Value = 0;
    }
    
    private CorruptionCounter(int value)
    {
        if (value < 0 || value > 12)
        {
            throw new ArgumentOutOfRangeException();
        }
        
        Value = value;
    }

    //A.K.A. UseTheOneRing
    public CorruptionCounter TakeDamage(int value)
    {
        var newCorruptionValue = Value + value;

        if (newCorruptionValue > 12)
        {
            newCorruptionValue = 12;
        }

        return new CorruptionCounter(newCorruptionValue);
    }
    
    public CorruptionCounter Heal(int value)
    {
        var newCorruptionValue = Value - value;

        if (newCorruptionValue < 0)
        {
            newCorruptionValue = 0;
        }

        return new CorruptionCounter(newCorruptionValue);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
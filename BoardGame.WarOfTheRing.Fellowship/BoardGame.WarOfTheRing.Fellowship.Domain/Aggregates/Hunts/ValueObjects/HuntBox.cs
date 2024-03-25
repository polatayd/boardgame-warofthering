using BoardGame.WarOfTheRing.Fellowship.Domain.Base;

namespace BoardGame.WarOfTheRing.Fellowship.Domain.Aggregates.Hunts.ValueObjects;

public class HuntBox : ValueObject
{
    public int NumberOfEyeResultDice { get; private set; }
    public int NumberOfCharacterResultDice { get; private set; }

    public HuntBox()
    {
        NumberOfEyeResultDice = 0;
        NumberOfCharacterResultDice = 0;
    }

    private HuntBox(int numberOfEyeResultDice, int numberOfCharacterResultDice)
    {
        NumberOfEyeResultDice = numberOfEyeResultDice;
        NumberOfCharacterResultDice = numberOfCharacterResultDice;
    }

    public HuntBox Clear()
    {
        return new HuntBox();
    }

    public HuntBox PlaceEyeDie()
    {
        return new HuntBox(NumberOfEyeResultDice + 1, NumberOfCharacterResultDice);
    }
    
    public HuntBox PlaceCharacterDie()
    {
        return new HuntBox(NumberOfEyeResultDice, NumberOfCharacterResultDice + 1);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return NumberOfEyeResultDice;
        yield return NumberOfCharacterResultDice;
    }
}
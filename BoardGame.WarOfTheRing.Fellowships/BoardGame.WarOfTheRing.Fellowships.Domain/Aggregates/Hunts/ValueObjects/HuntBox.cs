using BoardGame.WarOfTheRing.Fellowships.Domain.Base;

namespace BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts.ValueObjects;

public class HuntBox : ValueObject
{
    private const int MaximumNumberOfDiceToRoll = 5;

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
        //TODO: Restriction of number of characters in fellowship.
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

    public int GetDiceToRollCountForRoll()
    {
        return NumberOfEyeResultDice >= MaximumNumberOfDiceToRoll
            ? MaximumNumberOfDiceToRoll
            : NumberOfEyeResultDice;
    }
    
    public int GetDiceToRollCountForReRoll(int rerollCount, int numberOfSuccessDiceResult)
    {
        var availableRollCount = GetDiceToRollCountForRoll();
        availableRollCount -= numberOfSuccessDiceResult;

        return Math.Max(0, Math.Min(availableRollCount, rerollCount));
    }
}
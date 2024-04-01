using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts.Exceptions;
using BoardGame.WarOfTheRing.Fellowships.Domain.Base;

namespace BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts.ValueObjects;

public class Hunt : ValueObject
{
    private const int InitialSuccessDiceResultOfHunt = 6;
    
    public HuntState State { get; init; }
    public int NumberOfSuccessfulDiceResult { get; set; }
    public int NumberOfRerollCount { get; set; }
    
    public Hunt()
    {
        State = HuntState.Empty;
        NumberOfSuccessfulDiceResult = 0;
        NumberOfRerollCount = 0;
    }

    private Hunt(HuntState huntState, int numberOfSuccessfulDiceResult, int numberOfRerollCount)
    {
        State = huntState;
        if (huntState == HuntState.Empty)
        {
            NumberOfSuccessfulDiceResult = 0;
            NumberOfRerollCount = 0;
        }
        else
        {
            NumberOfSuccessfulDiceResult = numberOfSuccessfulDiceResult;
            NumberOfRerollCount = numberOfRerollCount;
        }
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return State;
        yield return NumberOfSuccessfulDiceResult;
    }
    
    public bool IsInAnyRollState()
    {
        return IsInRollState() || IsInReRollState();
    }
    
    public bool IsInRollState()
    {
        return State == HuntState.RollDice;
    }
    
    public bool IsInReRollState()
    {
        return State == HuntState.ReRollDice;
    }

    public Hunt Start()
    {
        if (State != HuntState.Empty)
        {
            throw new HuntStateException("Hunt can not be started if it's already active");
        }
        
        return new Hunt(HuntState.RollDice, NumberOfSuccessfulDiceResult, NumberOfRerollCount);
    }

    public Hunt CalculateSuccessRolls(IEnumerable<int> diceResults, int huntBoxNumberOfCharacterResultDice)
    {
        if (!IsInAnyRollState())
        {
            throw new HuntStateException("Hunt is not available for roll");
        }

        var successResult = InitialSuccessDiceResultOfHunt - huntBoxNumberOfCharacterResultDice;
        successResult = successResult > 1 ? successResult : 1;

        var successCount = diceResults.Count(x => x >= successResult);

        return new Hunt(State, NumberOfSuccessfulDiceResult + successCount, NumberOfRerollCount);
    }

    public Hunt CalculateNextHuntMoveAfterRoll(int diceToReRollCount, int rerollCount)
    {
        if (!IsInRollState())
        {
            throw new HuntStateException("Hunt is not available for roll");
        }

        if (diceToReRollCount != 0)
        {
            return new Hunt(HuntState.ReRollDice, NumberOfSuccessfulDiceResult, rerollCount);
        }
        
        return NumberOfSuccessfulDiceResult > 0 ? new Hunt(HuntState.DrawHuntTile, NumberOfSuccessfulDiceResult, NumberOfRerollCount) : new Hunt();
    }
    
    public Hunt CalculateNextHuntMoveAfterReRoll()
    {
        if (!IsInReRollState())
        {
            throw new HuntStateException("Hunt is not available for roll");
        }
        
        return NumberOfSuccessfulDiceResult > 0 ? new Hunt(HuntState.DrawHuntTile, NumberOfSuccessfulDiceResult, NumberOfRerollCount) : new Hunt();
    }
}
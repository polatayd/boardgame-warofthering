using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts.Exceptions;
using BoardGame.WarOfTheRing.Fellowships.Domain.Base;

namespace BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts.ValueObjects;

public class Hunt : ValueObject
{
    private const int InitialSuccessDiceResultOfHunt = 6;
    
    public HuntState State { get; init; }
    public int NumberOfSuccessfulDiceResult { get; set; }
    
    public Hunt()
    {
        State = HuntState.Empty;
        NumberOfSuccessfulDiceResult = 0;
    }

    private Hunt(HuntState huntState, int numberOfSuccessfulDiceResult)
    {
        State = huntState;
        if (huntState == HuntState.Empty)
        {
            NumberOfSuccessfulDiceResult = 0;
        }
        else
        {
            NumberOfSuccessfulDiceResult = numberOfSuccessfulDiceResult;
        }
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return State;
        yield return NumberOfSuccessfulDiceResult;
    }

    public Hunt Start()
    {
        if (State != HuntState.Empty)
        {
            throw new HuntStateException("Hunt can not be started if it's already active");
        }
        
        return new Hunt(HuntState.RollDice, NumberOfSuccessfulDiceResult);
    }

    public bool IsInAnyRollState()
    {
        return State == HuntState.RollDice || State == HuntState.ReRollDice;
    }
    
    public bool IsInRollState()
    {
        return State == HuntState.RollDice;
    }

    public Hunt CalculateSuccessRolls(List<int> diceResults, int huntBoxNumberOfCharacterResultDice)
    {
        if (!IsInAnyRollState())
        {
            throw new HuntStateException("Hunt is not available for roll");
        }

        var successResult = InitialSuccessDiceResultOfHunt - huntBoxNumberOfCharacterResultDice;
        successResult = successResult > 1 ? successResult : 1;

        var successCount = diceResults.Count(x => x >= successResult);

        return new Hunt(State, NumberOfSuccessfulDiceResult + successCount);
    }

    public Hunt EvaluateNextHuntMoveAfterRoll(int diceToRollCount, bool rerollIsAvailable)
    {
        if (!IsInAnyRollState())
        {
            throw new HuntStateException("Hunt is not available for roll");
        }
        
        if (diceToRollCount == 0)
        {
            return NumberOfSuccessfulDiceResult > 0 ? new Hunt(HuntState.DrawHuntTile, NumberOfSuccessfulDiceResult) : new Hunt();
        }

        if (IsInRollState() && rerollIsAvailable)
        {
            return new Hunt(HuntState.ReRollDice, NumberOfSuccessfulDiceResult);
        }
        
        return NumberOfSuccessfulDiceResult > 0 ? new Hunt(HuntState.DrawHuntTile, NumberOfSuccessfulDiceResult) : new Hunt();
    }
}
using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts.Exceptions;
using BoardGame.WarOfTheRing.Fellowships.Domain.Base;

namespace BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts.ValueObjects;

public class Hunt : ValueObject
{
    private const int InitialSuccessDiceResultOfHunt = 6;

    public HuntState State { get; init; }
    public int NumberOfSuccessfulDiceResult { get; set; }
    public int AvailableReRollCount { get; set; }

    private Hunt()
    {
        State = HuntState.Empty;
        NumberOfSuccessfulDiceResult = 0;
        AvailableReRollCount = 0;
    }

    private Hunt(HuntState huntState, int numberOfSuccessfulDiceResult, int availableReRollCount)
    {
        State = huntState;
        if (huntState == HuntState.Empty)
        {
            NumberOfSuccessfulDiceResult = 0;
            AvailableReRollCount = 0;
        }
        else
        {
            NumberOfSuccessfulDiceResult = numberOfSuccessfulDiceResult;
            AvailableReRollCount = availableReRollCount;
        }
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return State;
        yield return NumberOfSuccessfulDiceResult;
        yield return AvailableReRollCount;
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

    public bool IsInEndedState()
    {
        return State == HuntState.Ended;
    }

    public Hunt Start()
    {
        if (State != HuntState.Empty && State != HuntState.Ended)
        {
            throw new HuntStateException("Hunt can not be started if it's already active");
        }

        return new Hunt(HuntState.RollDice, NumberOfSuccessfulDiceResult, AvailableReRollCount);
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

        return new Hunt(State, NumberOfSuccessfulDiceResult + successCount, AvailableReRollCount);
    }

    public Hunt CalculateNextHuntMoveAfterRoll(int diceToReRollCount, int availableReRollCount)
    {
        if (!IsInRollState())
        {
            throw new HuntStateException("Hunt is not available for roll");
        }

        return diceToReRollCount != 0
            ? new Hunt(HuntState.ReRollDice, NumberOfSuccessfulDiceResult, availableReRollCount)
            : EndRollState();
    }

    public Hunt CalculateNextHuntMoveAfterReRoll()
    {
        if (!IsInReRollState())
        {
            throw new HuntStateException("Hunt is not available for roll");
        }

        return EndRollState();
    }

    private Hunt EndRollState()
    {
        return NumberOfSuccessfulDiceResult > 0
            ? new Hunt(HuntState.DrawHuntTile, NumberOfSuccessfulDiceResult, AvailableReRollCount)
            : new Hunt(HuntState.Ended, NumberOfSuccessfulDiceResult, AvailableReRollCount);
    }

    public static Hunt Create()
    {
        return new Hunt();
    }
}
using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts.Exceptions;
using BoardGame.WarOfTheRing.Fellowships.Domain.Base;

namespace BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts.ValueObjects;

public class Hunt : ValueObject
{
    private const int InitialSuccessDiceResultOfHunt = 6;

    public HuntState State { get; init; }
    public int NumberOfSuccessfulDiceResult { get; init; }
    public int AvailableReRollCount { get; init; }
    public HuntTile DrawnHuntTile { get; init; }

    private Hunt()
    {
        State = HuntState.Empty;
        NumberOfSuccessfulDiceResult = 0;
        AvailableReRollCount = 0;
        DrawnHuntTile = HuntTile.CreateNumberedTile(0);
    }

    private Hunt(HuntState huntState, int numberOfSuccessfulDiceResult, int availableReRollCount, HuntTile drawnHuntTile)
    {
        State = huntState;
        if (huntState == HuntState.Empty)
        {
            NumberOfSuccessfulDiceResult = 0;
            AvailableReRollCount = 0;
            DrawnHuntTile = HuntTile.CreateNumberedTile(0);
        }
        else
        {
            NumberOfSuccessfulDiceResult = numberOfSuccessfulDiceResult;
            AvailableReRollCount = availableReRollCount;
            DrawnHuntTile = drawnHuntTile;
        }
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return State;
        yield return NumberOfSuccessfulDiceResult;
        yield return AvailableReRollCount;
        yield return DrawnHuntTile;
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
    
    public bool IsInDrawHuntTileState()
    {
        return State == HuntState.DrawHuntTile;
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

        return new Hunt(HuntState.RollDice, NumberOfSuccessfulDiceResult, AvailableReRollCount, DrawnHuntTile);
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

        return new Hunt(State, NumberOfSuccessfulDiceResult + successCount, AvailableReRollCount, DrawnHuntTile);
    }

    public Hunt CalculateNextHuntMoveAfterRoll(int diceToReRollCount, int availableReRollCount)
    {
        if (!IsInRollState())
        {
            throw new HuntStateException("Hunt is not available for roll");
        }

        return diceToReRollCount != 0
            ? new Hunt(HuntState.ReRollDice, NumberOfSuccessfulDiceResult, availableReRollCount, DrawnHuntTile)
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
            ? new Hunt(HuntState.DrawHuntTile, NumberOfSuccessfulDiceResult, AvailableReRollCount, DrawnHuntTile)
            : new Hunt(HuntState.Ended, NumberOfSuccessfulDiceResult, AvailableReRollCount, DrawnHuntTile);
    }
    
    public Hunt CalculateNextHuntMoveAfterDrawTile(HuntTile drawnHuntTile)
    {
        if (!IsInDrawHuntTileState())
        {
            throw new HuntStateException("Hunt is not available for draw hunt tile");
        }

        if (drawnHuntTile.HuntDamage > 0 || drawnHuntTile.HasEyeIcon)
        {
            return new Hunt(HuntState.TakeCasualty, NumberOfSuccessfulDiceResult, AvailableReRollCount, drawnHuntTile);
        }

        if (drawnHuntTile.HasRevealIcon)
        {
            return new Hunt(HuntState.Reveal, NumberOfSuccessfulDiceResult, AvailableReRollCount, drawnHuntTile);
        }
        
        return new Hunt(HuntState.Ended, NumberOfSuccessfulDiceResult, AvailableReRollCount, drawnHuntTile);
    }

    public static Hunt Create()
    {
        return new Hunt();
    }
}
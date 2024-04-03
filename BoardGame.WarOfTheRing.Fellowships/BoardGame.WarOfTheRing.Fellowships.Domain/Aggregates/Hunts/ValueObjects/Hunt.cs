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

    private Hunt(HuntState huntState, int numberOfSuccessfulDiceResult, int availableReRollCount,
        HuntTile drawnHuntTile)
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
    
    public static Hunt Create()
    {
        return new Hunt();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return State;
        yield return NumberOfSuccessfulDiceResult;
        yield return AvailableReRollCount;
        yield return DrawnHuntTile;
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
    
    public bool IsInTakeCasualtyState()
    {
        return State == HuntState.TakeCasualty;
    }
    
    private int CalculateSuccessRolls(IEnumerable<int> diceResults, int huntBoxNumberOfCharacterResultDice)
    {
        var successResult = InitialSuccessDiceResultOfHunt - huntBoxNumberOfCharacterResultDice;
        successResult = successResult > 1 ? successResult : 1;

        var successCount = diceResults.Count(x => x >= successResult);

        return NumberOfSuccessfulDiceResult + successCount;
    }
    
    private Hunt EndRollState(int numberOfSuccessDiceResult)
    {
        return numberOfSuccessDiceResult > 0
            ? new Hunt(HuntState.DrawHuntTile, numberOfSuccessDiceResult, AvailableReRollCount, DrawnHuntTile)
            : new Hunt(HuntState.Ended, numberOfSuccessDiceResult, AvailableReRollCount, DrawnHuntTile);
    }

    public Hunt Start()
    {
        if (State != HuntState.Empty && State != HuntState.Ended)
        {
            throw new HuntStateException("Hunt can not be started if it's already active");
        }

        return new Hunt(HuntState.RollDice, NumberOfSuccessfulDiceResult, AvailableReRollCount, DrawnHuntTile);
    }

    public Hunt CalculateNextHuntMoveAfterRollDice(IEnumerable<int> diceResults, int huntBoxNumberOfCharacterResultDice,
        int diceToReRollCount, int availableReRollCount)
    {
        if (!IsInRollState())
        {
            throw new HuntStateException("Hunt is not available for roll");
        }

        var numberOfSuccessDiceResult = CalculateSuccessRolls(diceResults, huntBoxNumberOfCharacterResultDice);
        
        return diceToReRollCount != 0
            ? new Hunt(HuntState.ReRollDice, numberOfSuccessDiceResult, availableReRollCount, DrawnHuntTile)
            : EndRollState(numberOfSuccessDiceResult);
    }

    public Hunt CalculateNextHuntMoveAfterReRollDice(IEnumerable<int> diceResults, int huntBoxNumberOfCharacterResultDice)
    {
        if (!IsInReRollState())
        {
            throw new HuntStateException("Hunt is not available for roll");
        }
        
        var numberOfSuccessDiceResult = CalculateSuccessRolls(diceResults, huntBoxNumberOfCharacterResultDice);

        return EndRollState(numberOfSuccessDiceResult);
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

    public Hunt CalculateNextHuntMoveAfterTakeCasualty()
    {
        if (!IsInTakeCasualtyState())
        {
            throw new HuntStateException("Hunt is not available for take casualty");
        }
        
        if (DrawnHuntTile.HasRevealIcon)
        {
            return new Hunt(HuntState.Reveal, NumberOfSuccessfulDiceResult, AvailableReRollCount, DrawnHuntTile);
        }

        return new Hunt(HuntState.Ended, NumberOfSuccessfulDiceResult, AvailableReRollCount, DrawnHuntTile);
    }
    
    public int GetDamage()
    {
        if (DrawnHuntTile.HasEyeIcon)
        {
            return DrawnHuntTile.GetEyeDamage(NumberOfSuccessfulDiceResult);
        }

        return DrawnHuntTile.GetDamage();
    }
}
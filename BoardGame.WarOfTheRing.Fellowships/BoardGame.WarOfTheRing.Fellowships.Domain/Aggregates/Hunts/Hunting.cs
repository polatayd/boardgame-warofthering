using System.Diagnostics.CodeAnalysis;
using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts.Exceptions;
using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts.ValueObjects;
using BoardGame.WarOfTheRing.Fellowships.Domain.Base;

namespace BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts;

public class Hunting : EntityBase, IAggregateRoot
{
    public Guid FellowshipId { get; private set; }
    public Guid GameId { get; private set; }
    public HuntBox HuntBox { get; private set; }
    public HuntPool HuntPool { get; private set; }

    private Hunt activeHunt;

    public Hunt ActiveHunt
    {
        get => activeHunt;
        private set
        {
            activeHunt = value;
            PlaceCharacterDieIfNecessary();
        }
    }

    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    private Hunting()
    {
    }

    public Hunting(Guid huntingId, Guid fellowshipId, Guid gameId)
    {
        Id = huntingId;
        FellowshipId = fellowshipId;
        GameId = gameId;
        HuntBox = new HuntBox();
        HuntPool = HuntPool.Create();
        ActiveHunt = Hunt.Create();
    }

    public void StartActiveHunt()
    {
        ActiveHunt = ActiveHunt.Start();
    }

    public int GetDiceToRollCountForRoll()
    {
        if (!ActiveHunt.IsInRollState())
        {
            throw new HuntStateException("Hunt is not available for roll");
        }

        return HuntBox.GetDiceToRollCountForRoll();
    }

    public int GetDiceToRollCountForReRoll()
    {
        if (!ActiveHunt.IsInReRollState())
        {
            throw new HuntStateException("Hunt is not available for roll");
        }

        return HuntBox.GetDiceToRollCountForReRoll(ActiveHunt.AvailableReRollCount,
            ActiveHunt.NumberOfSuccessfulDiceResult);
    }

    public void CalculateSuccessRollsForRollDice(IEnumerable<int> diceResults, int availableReRollCount)
    {
        var diceToReRollCount =
            HuntBox.GetDiceToRollCountForReRoll(availableReRollCount, ActiveHunt.NumberOfSuccessfulDiceResult);

        ActiveHunt = ActiveHunt.CalculateNextHuntMoveAfterRollDice(diceResults, HuntBox.NumberOfCharacterResultDice,
            diceToReRollCount, availableReRollCount);
    }

    public void CalculateSuccessRollsForReRollDice(IEnumerable<int> diceResults)
    {
        ActiveHunt = ActiveHunt.CalculateNextHuntMoveAfterReRollDice(diceResults, HuntBox.NumberOfCharacterResultDice);
    }

    public HuntTile DrawHuntTile()
    {
        var (drawnHuntTile, huntPool) = HuntPool.DrawHuntTile();

        HuntPool = huntPool;
        ActiveHunt = ActiveHunt.CalculateNextHuntMoveAfterDrawTile(drawnHuntTile);

        return drawnHuntTile;
    }
    
    public void CompleteTakeCasualty()
    {
        ActiveHunt = ActiveHunt.CalculateNextHuntMoveAfterTakeCasualty();
    }

    private void PlaceCharacterDieIfNecessary()
    {
        if (!ActiveHunt.IsInEndedState())
        {
            return;
        }

        HuntBox = HuntBox.PlaceCharacterDie();
        ActiveHunt = Hunt.Create();
    }

    public int GetDamage()
    {
        return ActiveHunt.GetDamage();
    }

    public void CompleteReveal()
    {
        ActiveHunt = ActiveHunt.CalculateNextHuntMoveAfterReveal();
    }
}
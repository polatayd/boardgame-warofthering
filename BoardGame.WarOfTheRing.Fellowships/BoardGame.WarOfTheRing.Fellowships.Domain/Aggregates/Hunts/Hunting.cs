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
    public Hunt ActiveHunt { get; private set; }

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
        ActiveHunt = new Hunt();
    }

    public void StartActiveHunt()
    {
        ActiveHunt = ActiveHunt.Start();
    }

    public int GetDiceToRollCount()
    {
        if (!ActiveHunt.IsInAnyRollState())
        {
            throw new HuntStateException("Hunt is not available for roll");
        }

        return HuntBox.GetDiceToRollCount(ActiveHunt.NumberOfSuccessfulDiceResult);
    }

    public void EvaluateRollResult(List<int> diceResults)
    {
        ActiveHunt = ActiveHunt.CalculateSuccessRolls(diceResults, HuntBox.NumberOfCharacterResultDice);
    }

    public bool IsAvailableForReRollCalculation()
    {
        return HuntBox.GetDiceToRollCount(ActiveHunt.NumberOfSuccessfulDiceResult) > 0 &&
               ActiveHunt.IsInRollState();
    }

    public void EvaluateNextHuntMove(bool rerollIsAvailable)
    {
        var diceToRollCount = HuntBox.GetDiceToRollCount(ActiveHunt.NumberOfSuccessfulDiceResult);
        ActiveHunt = ActiveHunt.EvaluateNextHuntMoveAfterRoll(diceToRollCount, rerollIsAvailable);
    }
}
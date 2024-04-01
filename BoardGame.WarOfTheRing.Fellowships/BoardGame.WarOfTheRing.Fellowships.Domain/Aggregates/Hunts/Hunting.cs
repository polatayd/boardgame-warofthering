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

        return HuntBox.GetDiceToRollCountForReRoll(ActiveHunt.NumberOfRerollCount, ActiveHunt.NumberOfSuccessfulDiceResult);
    }

    public void CalculateSuccessRolls(IEnumerable<int> diceResults)
    {
        ActiveHunt = ActiveHunt.CalculateSuccessRolls(diceResults, HuntBox.NumberOfCharacterResultDice);
    }

    public void CalculateNextHuntMoveAfterRoll(int rerollCount)
    {
        var diceToRollCount = HuntBox.GetDiceToRollCountForReRoll(rerollCount, ActiveHunt.NumberOfSuccessfulDiceResult);
        ActiveHunt = ActiveHunt.CalculateNextHuntMoveAfterRoll(diceToRollCount, rerollCount);
    }
    
    public void CalculateNextHuntMoveAfterReRoll()
    {
        ActiveHunt = ActiveHunt.CalculateNextHuntMoveAfterReRoll();
    }
}
using BoardGame.WarOfTheRing.Dice.Domain.Base;
using BoardGame.WarOfTheRing.Dice.Domain.ValueObjects;

namespace BoardGame.WarOfTheRing.Dice.Domain.Aggregates;

public class DicePool : EntityBase, IAggregateRoot
{
    private readonly List<Die> dice;
    public IEnumerable<Die> Dice => dice.AsReadOnly();

    public DicePool(DicePoolType dicePoolType, ushort numberOfDice)
    {
        Id = Guid.NewGuid();
        dice = DicePoolFactory.CreateDicePool(dicePoolType, numberOfDice);
    }

    public IEnumerable<DieFace> Roll()
    {
        return dice.Select(die => die.Roll()).ToList();
    }
}
using BoardGame.WarOfTheRing.Dice.Application.DicePools.Inputs;
using FluentValidation;

namespace BoardGame.WarOfTheRing.Dice.Application.DicePools.Validators;

public class RollDicePoolInputValidator : AbstractValidator<RollDicePoolInput>
{
    public RollDicePoolInputValidator()
    {
        RuleFor(x => x.NumberOfDice)
            .InclusiveBetween<RollDicePoolInput, ushort>(0, 5);

        RuleFor(x => x.DicePoolType).IsInEnum();
    }
}
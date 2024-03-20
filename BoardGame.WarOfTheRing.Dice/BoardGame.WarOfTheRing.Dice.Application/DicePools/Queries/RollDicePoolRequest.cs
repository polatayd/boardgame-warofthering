using BoardGame.WarOfTheRing.Dice.Application.DicePools.Inputs;
using BoardGame.WarOfTheRing.Dice.Application.DicePools.Outputs;
using BoardGame.WarOfTheRing.Dice.Domain.Aggregates;
using BoardGame.WarOfTheRing.Dice.Domain.ValueObjects;
using MediatR;

namespace BoardGame.WarOfTheRing.Dice.Application.DicePools.Queries;

public class RollDicePoolRequest(RollDicePoolInput rollDicePoolInput) : IRequest<RollDicePoolOutput>
{
    public RollDicePoolInput RollDicePoolInput { get; } = rollDicePoolInput;
}

public class RollDicePoolRequestHandler : IRequestHandler<RollDicePoolRequest, RollDicePoolOutput>
{
    public Task<RollDicePoolOutput> Handle(RollDicePoolRequest request, CancellationToken cancellationToken)
    {
        var dicePoolType = GetDicePoolType(request.RollDicePoolInput.DicePoolType);

        var dicePool = new DicePool(dicePoolType, request.RollDicePoolInput.NumberOfDice);
        var result = dicePool.Roll();

        var outputResult = result.Select<DieFace, DieFaceResult>(x => new DieFaceResult()
        {
            Name = x.Name,
            Value = x.Value
        });

        return Task.FromResult(new RollDicePoolOutput() { Results = outputResult.ToList() });
    }

    private static DicePoolType GetDicePoolType(DicePoolTypeInput dicePoolTypeInput)
    {
        return dicePoolTypeInput switch
        {
            DicePoolTypeInput.FreePeoplesActionDicePool => DicePoolType.FreePeoplesActionDicePool,
            DicePoolTypeInput.ShadowPlayerActionDicePool => DicePoolType.ShadowPlayerActionDicePool,
            DicePoolTypeInput.NumberedDicePool => DicePoolType.NumberedDicePool,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}
using BoardGame.WarOfTheRing.Dice.Application.DicePools.Inputs;
using BoardGame.WarOfTheRing.Dice.Application.DicePools.Queries;
using FluentAssertions;

namespace BoardGame.WarOfTheRing.Dice.Tests.Integration;

public class RollDicePoolRequestTests
{
    [Fact]
    public async Task RollDicePool()
    {
        //Arrange
        var rollDicePoolHandler = new RollDicePoolRequestHandler();
        var rollDicePool = new RollDicePoolRequest(new RollDicePoolInput()
        {
            DicePoolType = DicePoolTypeInput.ShadowPlayerActionDicePool,
            NumberOfDice = 50
        });

        //Act
        var rollDicePoolOutput = await rollDicePoolHandler.Handle(rollDicePool, new CancellationToken());
        
        //Assert
        rollDicePoolOutput.Results.Should().HaveCount(50);
    }
}
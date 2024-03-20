using AutoFixture;
using BoardGame.WarOfTheRing.Dice.Domain.Aggregates;
using BoardGame.WarOfTheRing.Dice.Domain.ValueObjects;
using FluentAssertions;

namespace BoardGame.WarOfTheRing.Dice.Tests.Unit;

public class DicePoolTests()
{
    [Fact]
    public void CreateFreePeoplesDicePool()
    {
        //Arrange
        var fixture = new Fixture();
        var numberOfDice = fixture.Create<ushort>();

        //Act
        var sut = new DicePool(DicePoolType.FreePeoplesActionDicePool, numberOfDice);

        //Assert
        sut.Dice.Should().HaveCount(numberOfDice);
        sut.Dice.Should().AllBeAssignableTo<FreePeoplesActionDie>();
    }

    [Fact]
    public void CreateShadowDicePool()
    {
        //Arrange
        var fixture = new Fixture();
        var numberOfDice = fixture.Create<ushort>();

        //Act
        var sut = new DicePool(DicePoolType.ShadowPlayerActionDicePool, numberOfDice);

        //Assert
        sut.Dice.Should().HaveCount(numberOfDice);
        sut.Dice.Should().AllBeAssignableTo<ShadowActionDie>();
    }

    [Fact]
    public void CreateNumberedDicePool()
    {
        //Arrange
        var fixture = new Fixture();
        var numberOfDice = fixture.Create<ushort>() % 6;

        //Act
        var sut = new DicePool(DicePoolType.NumberedDicePool, (ushort)numberOfDice);

        //Assert
        sut.Dice.Should().HaveCount(numberOfDice);
        sut.Dice.Should().AllBeAssignableTo<NumberedDie>();
    }

    [Fact]
    public void Roll()
    {
        //Arrange
        const int numberOfDice = 5;
        var sut = new DicePool(DicePoolType.NumberedDicePool, numberOfDice);

        //Act
        var results = sut.Roll();

        //Assert
        results.Should().HaveCount(numberOfDice);
    }
}
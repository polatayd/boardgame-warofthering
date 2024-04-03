using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts.Exceptions;
using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts.ValueObjects;
using FluentAssertions;

namespace BoardGame.WarOfTheRing.Fellowships.Tests.HuntingTests;

public class HuntTests
{
    [Fact]
    public void Should_Create_Empty_Hunt()
    {
        //Arrange
        //Act
        var sut = Hunt.Create();

        //Assert
        sut.State.Should().Be(HuntState.Empty);
        sut.NumberOfSuccessfulDiceResult.Should().Be(0);
        sut.AvailableReRollCount.Should().Be(0);
    }
    
    [Fact]
    public void Should_Start()
    {
        //Arrange
        var sut = Hunt.Create();
        
        //Act
        var sut2 = sut.Start();
        
        //Assert
        sut2.State.Should().Be(HuntState.RollDice);
    }
    
    [Fact]
    public void Should_Throw_Exception_If_It_Is_Not_In_Empty_State()
    {
        //Arrange
        var sut = Hunt.Create().Start();
        
        //Act
        var action = () => sut.Start();
        
        //Assert
        action.Should().Throw<HuntStateException>();
    }
    
    //TODO: Hunt tests should be written.
}
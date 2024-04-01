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
    
    [Fact]
    public void Should_Calculate_Success_Dice_Result()
    {
        //Arrange
        var sut = Hunt.Create().Start();
        var results = new List<int> { 1, 5, 6, 3, 6 };
        
        //Act
        var sut2 = sut.CalculateSuccessRolls(results, 0);
        
        //Assert
        sut2.NumberOfSuccessfulDiceResult.Should().Be(2);
    }
    
    [Fact]
    public void Should_Calculate_Success_Dice_Result_When_There_Is_Character_Dice()
    {
        //Arrange
        var sut = Hunt.Create().Start();
        var results = new List<int> { 1, 5, 6, 3, 6 };
        
        //Act
        var sut2 = sut.CalculateSuccessRolls(results, 1);
        
        //Assert
        sut2.NumberOfSuccessfulDiceResult.Should().Be(3);
    }
    
    [Fact]
    public void Should_Calculate_Success_Dice_Result_When_All_Dice_Results_Are_Successful()
    {
        //Arrange
        var sut = Hunt.Create().Start();
        var results = new List<int> { 1, 5, 6 };
        
        //Act
        var sut2 = sut.CalculateSuccessRolls(results, 5);
        
        //Assert
        sut2.NumberOfSuccessfulDiceResult.Should().Be(3);
    }
    
    [Fact]
    public void Should_Calculate_Success_Dice_Result_When_There_Is_More_Character_Dice_Than_Dice_Result()
    {
        //Arrange
        var sut = Hunt.Create().Start();
        var results = new List<int> { 1, 5, 6 };
        
        //Act
        var sut2 = sut.CalculateSuccessRolls(results, 6);
        
        //Assert
        sut2.NumberOfSuccessfulDiceResult.Should().Be(3);
    }
    
    [Fact]
    public void Calculate_Should_Throw_Exception_When_It_Is_Not_In_Roll_State()
    {
        //Arrange
        var sut = Hunt.Create();
        var results = new List<int> { 1, 5, 6 };
        
        //Act
        var action = () => sut.CalculateSuccessRolls(results, 6);
        
        //Assert
        action.Should().Throw<HuntStateException>();
    }
    
    [Fact]
    public void Should_Calculate_Success_Dice_Result_If_It_Is_In_ReRoll_State()
    {
        //Arrange
        var sut = Hunt.Create().Start().CalculateNextHuntMoveAfterRoll(5, 1);
        var results = new List<int> { 1, 5, 6, 3, 6 };
        
        //Act
        var sut2 = sut.CalculateSuccessRolls(results, 0);
        
        //Assert
        sut2.NumberOfSuccessfulDiceResult.Should().Be(2);
    }
    
    [Fact]
    public void Should_Change_State_To_ReRoll()
    {
        //Arrange
        var sut = Hunt.Create().Start();
        
        //Act
        var sut2 = sut.CalculateNextHuntMoveAfterRoll(5, 3);
        
        //Assert
        sut2.State.Should().Be(HuntState.ReRollDice);
    }
    
    [Fact]
    public void Should_Change_State_To_Ended_From_Roll()
    {
        //Arrange
        var sut = Hunt.Create().Start();
        
        //Act
        var sut2 = sut.CalculateNextHuntMoveAfterRoll(0, 3);
        
        //Assert
        sut2.State.Should().Be(HuntState.Ended);
    }
    
    [Fact]
    public void Should_Change_State_To_DrawHuntTile_From_Roll()
    {
        //Arrange
        var results = new List<int> { 1, 5, 6, 3, 6 };
        var sut = Hunt.Create().Start().CalculateSuccessRolls(results, 0);

        //Act
        var sut2 = sut.CalculateNextHuntMoveAfterRoll(0, 3);
        
        //Assert
        sut2.State.Should().Be(HuntState.DrawHuntTile);
    }
    
    [Fact]
    public void Calculate_Next_Hunt_Should_Throw_Exception_If_It_Is_Not_In_Roll_State()
    {
        //Arrange
        var sut = Hunt.Create();

        //Act
        var action = () => sut.CalculateNextHuntMoveAfterRoll(0, 3);
        
        //Assert
        action.Should().Throw<HuntStateException>();
    }
    
    [Fact]
    public void Should_Change_State_To_Ended_From_ReRoll()
    {
        //Arrange
        var sut = Hunt.Create().Start().CalculateNextHuntMoveAfterRoll(5, 3);;
        
        //Act
        var sut2 = sut.CalculateNextHuntMoveAfterReRoll();
        
        //Assert
        sut2.State.Should().Be(HuntState.Ended);
    }
    
    [Fact]
    public void Should_Change_State_To_DrawHuntTile_From_ReRoll()
    {
        //Arrange
        var results = new List<int> { 1, 5, 6, 3, 6 };
        var sut = Hunt.Create().Start().CalculateSuccessRolls(results, 0)
            .CalculateNextHuntMoveAfterRoll(3, 3);;

        //Act
        var sut2 = sut.CalculateNextHuntMoveAfterReRoll();
        
        //Assert
        sut2.State.Should().Be(HuntState.DrawHuntTile);
    }
    
    [Fact]
    public void Calculate_Next_Hunt_ReRoll_Should_Throw_Exception_If_It_Is_Not_In_Roll_State()
    {
        //Arrange
        var sut = Hunt.Create();

        //Act
        var action = () => sut.CalculateNextHuntMoveAfterReRoll();
        
        //Assert
        action.Should().Throw<HuntStateException>();
    }
}
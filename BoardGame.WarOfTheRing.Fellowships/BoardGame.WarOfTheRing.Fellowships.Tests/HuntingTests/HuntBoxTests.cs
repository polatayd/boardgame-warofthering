using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts.ValueObjects;
using FluentAssertions;

namespace BoardGame.WarOfTheRing.Fellowships.Tests.HuntingTests;

public class HuntBoxTests
{
    [Fact]
    public void Should_Create_Empty_HuntBox()
    {
        //Arrange
        //Act
        var sut = new HuntBox();

        //Assert
        sut.NumberOfCharacterResultDice.Should().Be(0);
        sut.NumberOfEyeResultDice.Should().Be(0);
    }

    [Fact]
    public void Should_Place_Eye_Die_In_HuntBox()
    {
        //Arrange
        var sut = new HuntBox();

        //Act
        var newSut = sut.PlaceEyeDie();

        //Assert
        newSut.NumberOfEyeResultDice.Should().Be(1);
    }

    [Fact]
    public void Should_Place_Character_Die_In_HuntBox()
    {
        //Arrange
        var sut = new HuntBox();

        //Act
        var newSut = sut.PlaceCharacterDie();

        //Assert
        newSut.NumberOfCharacterResultDice.Should().Be(1);
    }

    [Fact]
    public void Should_Be_Equal_Another_HuntBox()
    {
        //Arrange
        var sut = new HuntBox();

        //Act
        var newSut = sut.PlaceCharacterDie();
        var newSut2 = sut.PlaceCharacterDie();

        //Assert
        newSut.Should().Be(newSut2);
    }

    [Fact]
    public void Should_Return_Dice_To_Roll()
    {
        //Arrange
        var sut = new HuntBox()
            .PlaceCharacterDie()
            .PlaceCharacterDie()
            .PlaceEyeDie()
            .PlaceEyeDie()
            .PlaceEyeDie();

        //Act
        var diceCount = sut.GetDiceToRollCountForRoll();

        //Assert
        diceCount.Should().Be(3);
    }
    
    [Fact]
    public void Should_Return_Dice_To_Roll_When_Eye_Dice_More_Than_Maximum()
    {
        //Arrange
        var sut = new HuntBox()
            .PlaceCharacterDie()
            .PlaceEyeDie()
            .PlaceEyeDie()
            .PlaceEyeDie()
            .PlaceEyeDie()
            .PlaceEyeDie()
            .PlaceEyeDie();

        //Act
        var diceCount = sut.GetDiceToRollCountForRoll();

        //Assert
        diceCount.Should().Be(5);
    }
    
    [Fact]
    public void Should_Return_Zero_When_There_Is_No_Eye_Dice()
    {
        //Arrange
        var sut = new HuntBox()
            .PlaceCharacterDie();

        //Act
        var diceCount = sut.GetDiceToRollCountForRoll();

        //Assert
        diceCount.Should().Be(0);
    }
    
    [Fact]
    public void Should_Return_Dice_To_ReRoll()
    {
        //Arrange
        var sut = new HuntBox()
            .PlaceCharacterDie()
            .PlaceEyeDie()
            .PlaceEyeDie()
            .PlaceEyeDie();

        //Act
        var diceCount = sut.GetDiceToRollCountForReRoll(1, 0);

        //Assert
        diceCount.Should().Be(1);
    }
    
    [Fact]
    public void Should_Return_Dice_To_ReRoll_When_There_Is_Success_Dice_Before()
    {
        //Arrange
        var sut = new HuntBox()
            .PlaceCharacterDie()
            .PlaceEyeDie()
            .PlaceEyeDie()
            .PlaceEyeDie();

        //Act
        var diceCount = sut.GetDiceToRollCountForReRoll(1, 1);

        //Assert
        diceCount.Should().Be(1);
    }
    
    [Fact]
    public void Should_Return_Dice_To_ReRoll_When_There_Is_More_Reroll_Count_Than_Available()
    {
        //Arrange
        var sut = new HuntBox()
            .PlaceCharacterDie()
            .PlaceEyeDie()
            .PlaceEyeDie()
            .PlaceEyeDie();

        //Act
        var diceCount = sut.GetDiceToRollCountForReRoll(3, 1);

        //Assert
        diceCount.Should().Be(2);
    }
    
    [Fact]
    public void Should_Return_Dice_To_ReRoll_When_There_Is_No_Available_Dice()
    {
        //Arrange
        var sut = new HuntBox()
            .PlaceCharacterDie()
            .PlaceEyeDie()
            .PlaceEyeDie()
            .PlaceEyeDie();

        //Act
        var diceCount = sut.GetDiceToRollCountForReRoll(3, 3);

        //Assert
        diceCount.Should().Be(0);
    }
    
    [Fact]
    public void Should_Return_Dice_To_ReRoll_When_There_Is_More_Eye_Dice_Than_Available()
    {
        //Arrange
        var sut = new HuntBox()
            .PlaceCharacterDie()
            .PlaceEyeDie()
            .PlaceEyeDie()
            .PlaceEyeDie()
            .PlaceEyeDie()
            .PlaceEyeDie()
            .PlaceEyeDie()
            .PlaceEyeDie();

        //Act
        var diceCount = sut.GetDiceToRollCountForReRoll(5, 1);

        //Assert
        diceCount.Should().Be(4);
    }
    
    [Fact]
    public void Should_Return_Zero_When_Reroll_Count_Is_Zero()
    {
        //Arrange
        var sut = new HuntBox()
            .PlaceCharacterDie()
            .PlaceEyeDie()
            .PlaceEyeDie()
            .PlaceEyeDie();

        //Act
        var diceCount = sut.GetDiceToRollCountForReRoll(0, 1);

        //Assert
        diceCount.Should().Be(0);
    }
}
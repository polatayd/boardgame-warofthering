using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts.Exceptions;
using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Hunts.ValueObjects;
using FluentAssertions;

namespace BoardGame.WarOfTheRing.Fellowships.Tests.HuntingTests;

public class HuntTileTests
{
    [Fact]
    public void Should_Create_Numbered()
    {
        //Arrange
        //Act
        var sut = HuntTile.CreateNumberedTile(2);

        //Assert
        sut.HuntDamage.Should().Be(2);
        sut.HasEyeIcon.Should().BeFalse();
        sut.HasDieIcon.Should().BeFalse();
    }
    
    [Fact]
    public void Should_Create_Numbered_With_Reveal_Icon()
    {
        //Arrange
        //Act
        var sut = HuntTile.CreateNumberedTile(2).WithRevealIcon();

        //Assert
        sut.HasRevealIcon.Should().BeTrue();
        sut.HasStopIcon.Should().BeFalse();
    }
    
    [Fact]
    public void Should_Create_Numbered_With_Stop_Icon()
    {
        //Arrange
        //Act
        var sut = HuntTile.CreateNumberedTile(2).WithStopIcon();

        //Assert
        sut.HasRevealIcon.Should().BeFalse();
        sut.HasStopIcon.Should().BeTrue();
    }
    
    [Fact]
    public void Should_Create_Eye()
    {
        //Arrange
        //Act
        var sut = HuntTile.CreateEyeTile();

        //Assert
        sut.HuntDamage.Should().Be(0);
        sut.HasEyeIcon.Should().BeTrue();
        sut.HasDieIcon.Should().BeFalse();
    }
    
    [Fact]
    public void Should_Create_Die()
    {
        //Arrange
        //Act
        var sut = HuntTile.CreateDieTile();

        //Assert
        sut.HuntDamage.Should().Be(0);
        sut.HasEyeIcon.Should().BeFalse();
        sut.HasDieIcon.Should().BeTrue();
    }
    
    [Fact]
    public void Should_Throw_Exception_If_It_Is_Reveal_Stop()
    {
        //Arrange
        var sut = HuntTile.CreateNumberedTile(2);

        //Act
        var action = () => sut.WithRevealIcon().WithStopIcon();

        //Assert
        action.Should().Throw<HuntTileCreationError>();
    }
    
    [Fact]
    public void Should_Return_Damage_For_Numbered()
    {
        //Arrange
        const int sutDamage = 2;
        var sut = HuntTile.CreateNumberedTile(sutDamage);

        //Act
        var damage = sut.GetDamage();

        //Assert
        damage.Should().Be(sutDamage);
    }
    
    [Fact]
    public void Get_Damage_Should_Throw_Exception_If_Eye_Tile()
    {
        //Arrange
        var sut = HuntTile.CreateEyeTile();

        //Act
        var action = () => sut.GetDamage();

        //Assert
        action.Should().Throw<HuntTileDamageCalculationError>();
    }
    
    [Fact]
    public void Get_Damage_Should_Throw_Exception_If_Die_Tile()
    {
        //Arrange
        var sut = HuntTile.CreateDieTile();

        //Act
        var action = () => sut.GetDamage();

        //Assert
        action.Should().Throw<HuntTileDamageCalculationError>();
    }
    
    [Fact]
    public void Should_Return_Damage_For_Eye()
    {
        //Arrange
        const int sutDamage = 2;
        var sut = HuntTile.CreateEyeTile();

        //Act
        var damage = sut.GetEyeDamage(sutDamage);

        //Assert
        damage.Should().Be(sutDamage);
    }
    
    [Fact]
    public void Get_Eye_Damage_Should_Throw_Exception_If_Not_Eye_Tile()
    {
        //Arrange
        var sut = HuntTile.CreateNumberedTile(2);

        //Act
        var action = () => sut.GetEyeDamage(2);

        //Assert
        action.Should().Throw<HuntTileDamageCalculationError>();
    }
    
    [Fact]
    public void Should_Return_Damage_For_Die()
    {
        //Arrange
        const int sutDamage = 2;
        var sut = HuntTile.CreateDieTile();

        //Act
        var damage = sut.GetDieDamage(sutDamage);

        //Assert
        damage.Should().Be(sutDamage);
    }
    
    [Fact]
    public void Get_Die_Damage_Should_Throw_Exception_If_Not_Die_Tile()
    {
        //Arrange
        var sut = HuntTile.CreateNumberedTile(2);

        //Act
        var action = () => sut.GetDieDamage(2);

        //Assert
        action.Should().Throw<HuntTileDamageCalculationError>();
    }
}
using AutoFixture;
using BoardGame.WarOfTheRing.PoliticalTrack.Domain.Aggregates.Nations;
using BoardGame.WarOfTheRing.PoliticalTrack.Domain.Aggregates.Nations.Exceptions;
using BoardGame.WarOfTheRing.PoliticalTrack.Domain.Aggregates.Nations.ValueObjects;
using FluentAssertions;

namespace BoardGame.WarOfTheRing.PoliticalTrack.Tests;

public class NationTest
{
    [Fact]
    public void AdvanceAtWarStatus()
    {
        //Arrange
        var fixture = new Fixture();
        
        var status = Status.Active;
        var position = new Position(2);
        var name = fixture.Create<Name>(); 
        var gameId = fixture.Create<Guid>(); 
        
        var sut = new Nation(status, position, name, gameId);

        //Act
        sut.AdvanceOnPoliticalTrack();
        
        //Assert
        sut.IsAtWar().Should().BeTrue();
    }
    
    [Fact]
    public void AdvanceNotAtWarStatus()
    {
        //Arrange
        var fixture = new Fixture();

        var status = Status.Passive;
        var position = new Position(1);
        var name = fixture.Create<Name>(); 
        var gameId = fixture.Create<Guid>(); 
        
        var sut = new Nation(status, position, name, gameId);

        //Act
        sut.AdvanceOnPoliticalTrack();
        
        //Assert
        sut.IsAtWar().Should().BeFalse();
    }
    
    [Fact]
    public void NotAdvanceAtWarStatus()
    {
        //Arrange
        var fixture = new Fixture();

        var status = Status.Passive;
        var position = new Position(2);
        var name = fixture.Create<Name>(); 
        var gameId = fixture.Create<Guid>(); 
        
        var sut = new Nation(status, position, name, gameId);

        //Act
        var action = () => sut.AdvanceOnPoliticalTrack();
        
        //Assert
        action.Should().ThrowExactly<PoliticalTrackAdvanceException>();
    }
    
    [Fact]
    public void NotAdvanceIfAlreadyInAtWarStatus()
    {
        //Arrange
        var fixture = new Fixture();

        var status = Status.Active;
        var position = new Position(2);
        var name = fixture.Create<Name>(); 
        var gameId = fixture.Create<Guid>(); 
        
        var sut = new Nation(status, position, name, gameId);

        //Act
        sut.AdvanceOnPoliticalTrack();
        var action = () => sut.AdvanceOnPoliticalTrack();
        
        //Assert
        action.Should().ThrowExactly<PoliticalTrackAdvanceException>();
    }
    
    [Fact]
    public void NotCreateNationIsAlreadyInAtWarPosition()
    {
        //Arrange
        var fixture = new Fixture();
        
        var status = Status.Passive;
        var position = new Position(3);
        var name = fixture.Create<Name>(); 
        var gameId = fixture.Create<Guid>(); 
        
        //Act
        var action = () => new Nation(status, position, name, gameId);
        
        //Assert
        action.Should().ThrowExactly<ArgumentOutOfRangeException>();
    }
    
    [Theory]
    [InlineData(-1)]
    [InlineData(4)]
    public void NotCreatePositionOutOfRange(int position)
    {
        //Arrange
        //Act
        var action = () => new Position(position);
        
        //Assert
        action.Should().ThrowExactly<ArgumentOutOfRangeException>();
    }
    
    [Fact]
    public void MustActivate()
    {
        //Arrange
        var fixture = new Fixture();
        
        var status = Status.Passive;
        var position = new Position(1);
        var name = fixture.Create<Name>(); 
        var gameId = fixture.Create<Guid>(); 
        
        var sut = new Nation(status, position, name, gameId);

        //Act
        sut.Activate();
        
        //Assert
        sut.Status.Should().Be(Status.Active);
    }
}
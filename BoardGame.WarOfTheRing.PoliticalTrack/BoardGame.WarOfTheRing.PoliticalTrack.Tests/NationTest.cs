using BoardGame.WarOfTheRing.PoliticalTrack.Domain.Aggregates;
using BoardGame.WarOfTheRing.PoliticalTrack.Domain.Aggregates.Exceptions;
using BoardGame.WarOfTheRing.PoliticalTrack.Domain.ValueObjects;
using FluentAssertions;

namespace BoardGame.WarOfTheRing.PoliticalTrack.Tests;

public class NationTest
{
    [Fact]
    public void AdvanceAtWarStatus()
    {
        //Arrange
        var status = Status.Active;
        var track = new Track(2);
        
        var sut = new Nation(status, track);

        //Act
        sut.AdvanceOnPoliticalTrack();
        
        //Assert
        sut.IsAtWar().Should().BeTrue();
    }
    
    [Fact]
    public void AdvanceNotAtWarStatus()
    {
        //Arrange
        var status = Status.Passive;
        var track = new Track(1);
        
        var sut = new Nation(status, track);

        //Act
        sut.AdvanceOnPoliticalTrack();
        
        //Assert
        sut.IsAtWar().Should().BeFalse();
    }
    
    [Fact]
    public void NotAdvanceAtWarStatus()
    {
        //Arrange
        var status = Status.Passive;
        var track = new Track(2);
        
        var sut = new Nation(status, track);

        //Act
        var action = () => sut.AdvanceOnPoliticalTrack();
        
        //Assert
        action.Should().ThrowExactly<PoliticalTrackAdvanceException>();
    }
    
    [Fact]
    public void NotAdvanceIfAlreadyInAtWarStatus()
    {
        //Arrange
        var status = Status.Active;
        var track = new Track(3);
        
        var sut = new Nation(status, track);

        //Act
        var action = () => sut.AdvanceOnPoliticalTrack();
        
        //Assert
        action.Should().ThrowExactly<PoliticalTrackAdvanceException>();
    }
    
    [Fact]
    public void NotCreateNationIsAlreadyInAtWarPosition()
    {
        //Arrange
        var status = Status.Passive;
        var track = new Track(3);
        
        //Act
        var action = () => new Nation(status, track);
        
        //Assert
        action.Should().ThrowExactly<ArgumentOutOfRangeException>();
    }
    
    [Theory]
    [InlineData(-1)]
    [InlineData(4)]
    public void NotCreateTrackOutOfRange(int position)
    {
        //Arrange
        
        //Act
        var action = () => new Track(position);
        
        //Assert
        action.Should().ThrowExactly<ArgumentOutOfRangeException>();
    }
    
    [Fact]
    public void MustActivate()
    {
        //Arrange
        var status = Status.Passive;
        var track = new Track(1);
        
        var sut = new Nation(status, track);

        //Act
        sut.Activate();
        
        //Assert
        sut.Status.Should().Be(Status.Active);
    }
}
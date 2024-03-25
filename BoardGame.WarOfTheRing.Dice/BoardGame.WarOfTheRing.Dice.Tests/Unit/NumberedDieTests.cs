using BoardGame.WarOfTheRing.Dice.Domain.Aggregates.DicePools.ValueObjects;
using FluentAssertions;

namespace BoardGame.WarOfTheRing.Dice.Tests.Unit;

public class NumberedDieTests
{
    private readonly List<DieFace> validFaces =
    [
        DieFace.OneDieFace,
        DieFace.TwoDieFace,
        DieFace.ThreeDieFace,
        DieFace.FourDieFace,
        DieFace.FiveDieFace,
        DieFace.SixDieFace,
    ];

    [Fact]
    public void DieMustHaveSixFaces()
    {
        //Arrange
        var sut = NumberedDie.Create();

        //Act
        var faces = sut.Faces;

        //Assert
        faces.Should().HaveCount(6);
    }
    
    [Fact]
    public void TwoDiceMustBeSame()
    {
        //Arrange
        var sut1 = NumberedDie.Create();
        var sut2 = NumberedDie.Create();

        //Act
        var isSame = sut1 == sut2;

        //Assert
        isSame.Should().BeTrue();
    }
    
    [Fact]
    public void DieMustRoll()
    {
        //Arrange
        var sut = NumberedDie.Create();

        //Act
        var rolledFace = sut.Roll();
        
        //Assert
        rolledFace.Should().BeOneOf(validFaces);
    }
    
    [Theory]
    [InlineData(10000)]
    public void DieMustReturnProbabilisticResultsWhenRolledNTimes(int numberOfRolls)
    {
        //Arrange
        var sut = NumberedDie.Create();
        var rolledFaces = new Dictionary<string, long>();
        const float probabilityDelta = 1.5f;
        
        //Act
        for (var i = 0; i < numberOfRolls; i++)
        {
            var rolledFace = sut.Roll();
            if (!rolledFaces.TryAdd(rolledFace.Name, 1))
            {
                rolledFaces[rolledFace.Name]++;
            }
        }

        //Assert
        foreach (var rolledFace in rolledFaces)
        {
            CalculateProbability(rolledFace.Value).Should().BeInRange(16.6f - probabilityDelta, 16.6f + probabilityDelta);
        }
        return;

        float CalculateProbability(float value)
        {
            return value * 100 / numberOfRolls;
        }
    }
}
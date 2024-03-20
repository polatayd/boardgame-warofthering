using BoardGame.WarOfTheRing.Dice.Domain.ValueObjects;
using FluentAssertions;

namespace BoardGame.WarOfTheRing.Dice.Tests.Unit;

public class ShadowActionDieTests
{
    private readonly List<DieFace> validFaces =
    [
        DieFace.EventDieFace,
        DieFace.CharacterDieFace,
        DieFace.MusterDieFace,
        DieFace.MusterArmyDieFace,
        DieFace.EyeDieFace,
        DieFace.ArmyDieFace,
    ];

    [Fact]
    public void DieMustHaveSixFaces()
    {
        //Arrange
        var sut = ShadowActionDie.Create();

        //Act
        var faces = sut.Faces;

        //Assert
        faces.Should().HaveCount(6);
    }
    
    [Fact]
    public void TwoDiceMustBeSame()
    {
        //Arrange
        var sut1 = ShadowActionDie.Create();
        var sut2 = ShadowActionDie.Create();

        //Act
        var isSame = sut1 == sut2;

        //Assert
        isSame.Should().BeTrue();
    }
    
    [Fact]
    public void DieMustRoll()
    {
        //Arrange
        var sut = ShadowActionDie.Create();

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
        var sut = ShadowActionDie.Create();
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
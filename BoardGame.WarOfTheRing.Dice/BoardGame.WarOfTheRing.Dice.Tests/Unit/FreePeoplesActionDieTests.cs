using BoardGame.WarOfTheRing.Dice.Domain.Aggregates.DicePools.ValueObjects;
using FluentAssertions;

namespace BoardGame.WarOfTheRing.Dice.Tests.Unit;

public class FreePeoplesActionDieTests
{
    private readonly List<DieFace> validFaces =
    [
        DieFace.EventDieFace,
        DieFace.CharacterDieFace,
        DieFace.MusterDieFace,
        DieFace.MusterArmyDieFace,
        DieFace.WillOfTheWestDieFace
    ];

    [Fact]
    public void DieMustHaveSixFaces()
    {
        //Arrange
        var sut = FreePeoplesActionDie.Create();

        //Act
        var faces = sut.Faces;

        //Assert
        faces.Should().HaveCount(6);
    }

    [Fact]
    public void DieMustHaveTwoCharacterFaces()
    {
        //Arrange
        var sut = FreePeoplesActionDie.Create();

        //Act
        var faces = sut.Faces;

        //Assert
        faces.Should().ContainInOrder(DieFace.CharacterDieFace, DieFace.CharacterDieFace);
        faces.Should().NotContainInOrder(DieFace.CharacterDieFace, DieFace.CharacterDieFace, DieFace.CharacterDieFace);
    }
    
    [Fact]
    public void TwoDiceMustBeSame()
    {
        //Arrange
        var sut1 = FreePeoplesActionDie.Create();
        var sut2 = FreePeoplesActionDie.Create();

        //Act
        var isSame = sut1 == sut2;

        //Assert
        isSame.Should().BeTrue();
    }

    [Fact]
    public void DieMustRoll()
    {
        //Arrange
        var sut = FreePeoplesActionDie.Create();

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
        var sut = FreePeoplesActionDie.Create();
        var rolledFaces = new Dictionary<string, long>();
        const float probabilityDelta = 1.6f;

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
            if (rolledFace.Key == DieFace.CharacterDieFace.Name)
            {
                CalculateProbability(rolledFace.Value).Should()
                    .BeInRange(2 * 16.6f - probabilityDelta, 2 * 16.6f + probabilityDelta);
            }
            else
            {
                CalculateProbability(rolledFace.Value).Should()
                    .BeInRange(16.6f - probabilityDelta, 16.6f + probabilityDelta);
            }
        }

        return;

        float CalculateProbability(float value)
        {
            return value * 100 / numberOfRolls;
        }
    }
}
using BoardGame.WarOfTheRing.Dice.Domain.Base;

namespace BoardGame.WarOfTheRing.Dice.Domain.Aggregates.DicePools.ValueObjects;

public class DieFace(string name, ushort value) : ValueObject
{
    public static readonly DieFace CharacterDieFace = new("Character", 0);
    public static readonly DieFace ArmyDieFace = new("Army", 0);
    public static readonly DieFace MusterDieFace = new("Muster", 0);
    public static readonly DieFace MusterArmyDieFace = new("MusterArmy", 0);
    public static readonly DieFace EventDieFace = new("Event", 0);
    public static readonly DieFace EyeDieFace = new("Eye", 0);
    public static readonly DieFace WillOfTheWestDieFace = new("WillOfTheWest", 0);
    public static readonly DieFace OneDieFace = new("One", 1);
    public static readonly DieFace TwoDieFace = new("Two", 2);
    public static readonly DieFace ThreeDieFace = new("Three", 3);
    public static readonly DieFace FourDieFace = new("Four", 4);
    public static readonly DieFace FiveDieFace = new("Five", 5);
    public static readonly DieFace SixDieFace = new("Six", 6);
    
    public string Name { get; } = name;
    public ushort Value { get; } = value;

    public override string ToString()
    {
        return Name;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return Value;
    }
}
using BoardGame.WarOfTheRing.Fellowships.Domain.Base;

namespace BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships.ValueObjects;

public class Character : ValueObject
{
    public static Character Gandalf { get; } = new Character(3, CharacterName.GandalfTheGrey);
    public static Character Strider { get; } = new Character(3, CharacterName.Strider);
    public static Character Boromir { get; } = new Character(2, CharacterName.Boromir);
    public static Character Gimli { get; } = new Character(2, CharacterName.Gimli);
    public static Character Legolas { get; } = new Character(2, CharacterName.Legolas);
    public static Character Pippin { get; } = new Character(1, CharacterName.PeregrinTook);
    public static Character Merry { get; } = new Character(1, CharacterName.MeriadocBrandybuck);
    public static Character Gollum { get; } = new Character(0, CharacterName.Gollum);
    
    public enum CharacterName {
        GandalfTheGrey,
        Strider,
        Boromir,
        Gimli,
        Legolas,
        PeregrinTook,
        MeriadocBrandybuck,
        Gollum
    }
    
    private Character(int level, CharacterName name)
    {
        Level = level;
        Name = name;
    }

    public int Level { get; init; }
    public CharacterName Name { get; init; }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Level;
        yield return Name;
    }
}
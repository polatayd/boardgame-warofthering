using BoardGame.WarOfTheRing.Dice.Domain.Base;

namespace BoardGame.WarOfTheRing.Dice.Domain.ValueObjects;

public abstract class Die : ValueObject
{
    public IReadOnlyList<DieFace> Faces => dieFaces.AsReadOnly();
    private readonly List<DieFace> dieFaces;

    protected Die(List<DieFace> dieFaces)
    {
        if (dieFaces is null || dieFaces.Count == 0)
        {
            throw new ArgumentException("Die faces cannot be null or empty");
        }

        this.dieFaces = dieFaces;
    }

    public DieFace Roll()
    {
        var random = new Random();
        return dieFaces[random.Next(dieFaces.Count)];
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        return dieFaces;
    }
}
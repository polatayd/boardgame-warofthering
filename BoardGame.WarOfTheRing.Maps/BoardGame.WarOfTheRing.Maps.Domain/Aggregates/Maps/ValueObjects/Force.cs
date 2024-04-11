using BoardGame.WarOfTheRing.Maps.Domain.Base;

namespace BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps.ValueObjects;

public class Force : ValueObject
{
    private Force()
    {
    }

    private Force(string name)
    {
        Name = name;
    }

    public static Force None()
    {
        return new Force(ForceTypes.None);
    }

    public static Force Shadow()
    {
        return new Force(ForceTypes.Shadow);
    }

    public static Force FreePeoples()
    {
        return new Force(ForceTypes.FreePeoples);
    }

    public static Force FromName(string name)
    {
        if (name == ForceTypes.Shadow)
        {
            return Shadow();
        }

        if (name == ForceTypes.FreePeoples)
        {
            return FreePeoples();
        }

        return None();
    }

    public string Name { get; init; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
    }
}

public static class ForceTypes
{
    public static string None => nameof(None);
    public static string FreePeoples => nameof(FreePeoples);
    public static string Shadow => nameof(Shadow);
}
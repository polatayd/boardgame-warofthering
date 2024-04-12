using BoardGame.WarOfTheRing.Maps.Domain.Base;

namespace BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps.ValueObjects;

public class Army : ValueObject
{
    private List<Unit> units = new();
    private List<Leader> leaders = new();
    public IReadOnlyList<Unit> Units => units.AsReadOnly();
    public IReadOnlyList<Leader> Leaders => leaders.AsReadOnly();

    private Army() {}
    private Army(List<Unit> units, List<Leader> leaders)
    {
        this.units = units;
        this.leaders = leaders;
    }

    public static Army Create()
    {
        return new Army();
    }

    public Army AddUnits(List<Unit> addingUnits)
    {
        units.AddRange(addingUnits);

        return new Army(units, leaders);
    }
    
    public Army AddLeaders(List<Leader> addingLeaders)
    {
        leaders.AddRange(addingLeaders);

        return new Army(units, leaders);
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        foreach (var unit in Units)
        {
            yield return unit;
        }
        
        foreach (var leader in Leaders)
        {
            yield return leader;
        }
    }
}
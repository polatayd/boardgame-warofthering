using BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps.Entities;
using BoardGame.WarOfTheRing.Maps.Domain.Base;

namespace BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps;

public class Map : EntityBase, IAggregateRoot
{
    private List<Region> regions = new();
    private List<Nation> nations = new();

    public IReadOnlyList<Region> Regions => regions.AsReadOnly();
    public IReadOnlyList<Nation> Nations => nations.AsReadOnly();

    public Map()
    {
        Id = Guid.NewGuid();
    }

    public void WithNations(List<Nation> nations)
    {
        this.nations = nations;
    }
    
    public void WithRegions(List<Region> regions)
    {
        this.regions = regions;
    }
}
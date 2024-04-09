using BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps.ValueObjects;
using BoardGame.WarOfTheRing.Maps.Domain.Base;

namespace BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps.Entities;

public class Region : EntityBase
{
    private List<Region> neighborRegions = new();

    public IReadOnlyList<Region> NeighborRegions => neighborRegions.AsReadOnly();
    public string Name { get; init; }
    public Terrain Terrain { get; init; }
    public Nation InBorderOf { get; init; }
    public Army Army { get; init; }
    
    public Region(string name, Terrain terrain, Nation inBorderOf)
    {
        Id = Guid.NewGuid();
        Name = name;
        Terrain = terrain;
        InBorderOf = inBorderOf;
        Army = Army.Empty;
    }
    
    public void AssignNeighbors(List<Region> regions)
    {
        neighborRegions.AddRange(regions);
    }
}
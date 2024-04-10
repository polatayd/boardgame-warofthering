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
    public Army Army { get; private set; }
    public Guid MapId { get; init; }

    private Region() {}
    public Region(string name, Terrain terrain, Nation inBorderOf, Guid mapId)
    {
        Id = Guid.NewGuid();
        Name = name;
        Terrain = terrain;
        InBorderOf = inBorderOf;
        MapId = mapId;
        Army = Army.Create();
    }
    
    public void AssignNeighbors(List<Region> regions)
    {
        neighborRegions.AddRange(regions);
    }
}
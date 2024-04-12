using BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps.ValueObjects;
using BoardGame.WarOfTheRing.Maps.Domain.Base;

namespace BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps.Entities;

public class Region : EntityBase
{
    private List<Neighbor> neighbors = new();
    
    public IReadOnlyList<Neighbor> Neighbors => neighbors.AsReadOnly();
    public string Name { get; init; }
    public Terrain Terrain { get; init; }
    public string InBorderOf { get; init; }
    public Army Army { get; private set; }
    public Guid MapId { get; init; }

    private Region() {}
    public Region(string name, Terrain terrain, string inBorderOf, List<Neighbor> neighbors, Army army, Guid mapId)
    {
        Id = Guid.NewGuid();
        Name = name;
        Terrain = terrain;
        InBorderOf = inBorderOf;
        MapId = mapId;
        Army = army;
        this.neighbors = neighbors;
    }
}
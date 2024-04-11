using BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps.ValueObjects;
using BoardGame.WarOfTheRing.Maps.Domain.Base;

namespace BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps.Entities;

public class Nation : EntityBase
{
    public const string Dwarves = "Dwarves";
    public const string Elves = "Elves";
    public const string Gondor  = "Gondor";
    public const string TheNorth = "TheNorth";
    public const string Rohan = "Rohan";
    public const string Isengard = "Isengard";
    public const string Sauron  = "Sauron";
    public const string Sauthrons = "Sauthrons";
    
    private List<Unit> reinforcements = new();
    
    public IReadOnlyList<Unit> Reinforcements => reinforcements.AsReadOnly();
    public string Name { get; init; }
    public Force BelongsTo { get; init; }
    public bool IsAtWar { get; init; }
    public Guid MapId { get; init; }
    
    private Nation() {}
    public Nation(string name, Force belongsTo, List<Unit> reinforcements, Guid mapId)
    {
        Id = Guid.NewGuid();
        Name = name;
        BelongsTo = belongsTo;
        MapId = mapId;
        this.reinforcements = reinforcements;
    }
}
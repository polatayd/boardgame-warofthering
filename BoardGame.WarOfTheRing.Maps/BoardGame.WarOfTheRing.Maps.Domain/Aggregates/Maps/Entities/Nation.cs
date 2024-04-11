using BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps.ValueObjects;
using BoardGame.WarOfTheRing.Maps.Domain.Base;

namespace BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps.Entities;

public class Nation : EntityBase
{
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

public static class NationNames
{
    public static string TheNorth => nameof(TheNorth);
    public static string Elves => nameof(Elves);
    public static string Dwarves => nameof(Dwarves);
    public static string Rohan => nameof(Rohan);
    public static string Gondor => nameof(Gondor);
    public static string Isengard => nameof(Isengard);
    public static string Southrons => nameof(Southrons);
    public static string Sauron => nameof(Sauron);
}
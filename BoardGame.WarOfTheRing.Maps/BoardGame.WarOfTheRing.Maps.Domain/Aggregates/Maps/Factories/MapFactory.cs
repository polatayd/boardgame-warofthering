using BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps.Entities;
using BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps.ValueObjects;

namespace BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps.Factories;

public static class MapFactory
{
    public static Nation CreateNation(string name, string belongsTo, int regularUnitCount, int eliteUnitCount,
        Guid mapId)
    {
        var units = CreateUnits(name, regularUnitCount, eliteUnitCount);

        return new Nation(name, Force.FromName(belongsTo), units, mapId);
    }

    public static Region CreateRegion(string name, string terrainType, string controlledBy, string inBorderOf,
        List<string> neighborRegions, string armyNationName, int armyRegularCount, int armyEliteCount, Guid mapId)
    {
        var neighbors = neighborRegions.Select(x => new Neighbor(x)).ToList();
        var terrain = CreateTerrain(terrainType, controlledBy);
        var army = Army.Create().AddUnits(CreateUnits(armyNationName, armyRegularCount, armyEliteCount));
        var region = new Region(name, terrain, inBorderOf, neighbors, army, mapId);
        return region;
    }

    public static Terrain CreateTerrain(string terrainType, string controlledBy)
    {
        if (terrainType == TerrainTypes.Empty)
        {
            return Terrain.CreateEmpty();
        }

        if (terrainType == TerrainTypes.Fortification)
        {
            return Terrain.CreateFortification();
        }

        if (terrainType == SettlementTypes.Town)
        {
            return Terrain.CreateSettlement(Settlement.Town(Force.FromName(controlledBy)));
        }

        if (terrainType == SettlementTypes.City)
        {
            return Terrain.CreateSettlement(Settlement.City(Force.FromName(controlledBy)));
        }

        if (terrainType == SettlementTypes.Stronghold)
        {
            return Terrain.CreateSettlement(Settlement.Stronghold(Force.FromName(controlledBy)));
        }

        throw new ArgumentException();
    }

    private static List<Unit> CreateUnits(string nationName, int regularUnitCount, int eliteUnitCount)
    {
        var units = new List<Unit>();

        for (var i = 0; i < regularUnitCount; i++)
        {
            units.Add(Unit.Create(nationName, UnitType.FromValue(UnitTypes.Regular)));
        }

        for (var i = 0; i < eliteUnitCount; i++)
        {
            units.Add(Unit.Create(nationName, UnitType.FromValue(UnitTypes.Elite)));
        }

        return units;
    }
}
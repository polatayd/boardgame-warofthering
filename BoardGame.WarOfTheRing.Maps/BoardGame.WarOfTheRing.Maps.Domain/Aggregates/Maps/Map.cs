using BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps.Entities;
using BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps.ValueObjects;
using BoardGame.WarOfTheRing.Maps.Domain.Base;

namespace BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps;

public class Map : EntityBase, IAggregateRoot
{
    private List<Region> regions = new();
    private List<Nation> nations = new();

    public IReadOnlyList<Region> Regions => regions.AsReadOnly();
    public IReadOnlyList<Nation> Nations => nations.AsReadOnly();

    private Map()
    {
        Id = Guid.NewGuid();
    }

    public static Map Create()
    {
        var map = new Map();

        //TODO: All these map configurations will be read from configuration file somehow??   
        map.CreateNations();
        map.CreateRegions();
        map.CreateReinforcements();

        return map;
    }

    private void CreateReinforcements()
    {
        CreateNationReinforcements(GetNation(Nation.Dwarves), 5, 5);
        CreateNationReinforcements(GetNation(Nation.Elves), 5, 10);
        CreateNationReinforcements(GetNation(Nation.Gondor), 15, 5);
        CreateNationReinforcements(GetNation(Nation.TheNorth), 10, 5);
        CreateNationReinforcements(GetNation(Nation.Rohan), 10, 5);
        CreateNationReinforcements(GetNation(Nation.Isengard), 12, 6);
        CreateNationReinforcements(GetNation(Nation.Sauthorns), 24, 6);
        CreateNationReinforcements(GetNation(Nation.Sauron), 36, 6);
    }

    private void CreateNationReinforcements(Nation nation, int regularUnitCount, int eliteUnitCount)
    {
        var units = new List<Unit>();
        for (var i = 0; i < regularUnitCount; i++)
        {
            units.Add(Unit.Create(nation.Name, UnitType.Regular()));
        }

        for (var i = 0; i < eliteUnitCount; i++)
        {
            units.Add(Unit.Create(nation.Name, UnitType.Elite()));
        }

        nation.AddToReinforcement(units);
    }

    private Nation GetNation(string name)
    {
        return nations.First(x => x.Name == name);
    }

    private Region GetRegion(string name)
    {
        return regions.First(x => x.Name == name);
    }

    private void CreateNations()
    {
        nations =
        [
            new Nation(Nation.Dwarves, Force.FreePeoples(), Id),
            new Nation(Nation.Elves, Force.FreePeoples(), Id),
            new Nation(Nation.Gondor, Force.FreePeoples(), Id),
            new Nation(Nation.TheNorth, Force.FreePeoples(), Id),
            new Nation(Nation.Rohan, Force.FreePeoples(), Id),
            new Nation(Nation.Isengard, Force.Shadow(), Id),
            new Nation(Nation.Sauron, Force.Shadow(), Id),
            new Nation(Nation.Sauthorns, Force.Shadow(), Id)
        ];
    }

    private void CreateRegions()
    {
        regions =
        [
            new Region("Dale", Terrain.CreateSettlement(Settlement.City(Force.FreePeoples())),
                GetNation(Nation.TheNorth), Id),
            new Region("WoodlandRealm", Terrain.CreateSettlement(Settlement.Stronghold(Force.FreePeoples())),
                GetNation(Nation.Elves), Id),
            new Region("Erebor", Terrain.CreateSettlement(Settlement.Stronghold(Force.FreePeoples())),
                GetNation(Nation.Dwarves), Id),
            new Region("IronHills", Terrain.CreateSettlement(Settlement.Town(Force.FreePeoples())),
                GetNation(Nation.Dwarves), Id),
            new Region("EastRhun", Terrain.CreateEmpty(), GetNation(Nation.Sauthorns), Id),
            new Region("NorthRhun", Terrain.CreateSettlement(Settlement.Town(Force.Shadow())),
                GetNation(Nation.Sauthorns), Id),
            new Region("ValeOfTheCarnen", Terrain.CreateEmpty(), null, Id),
            new Region("DolGuldur", Terrain.CreateSettlement(Settlement.Stronghold(Force.Shadow())),
                GetNation(Nation.Sauron), Id),
            new Region("Osgiliath", Terrain.CreateFortification(), null, Id)
        ];

        GetRegion("Dale").AssignNeighbors([
            GetRegion("WoodlandRealm"),
            GetRegion("Erebor"),
            GetRegion("IronHills"),
            GetRegion("ValeOfTheCarnen"),
        ]);

        GetRegion("WoodlandRealm").AssignNeighbors([
            GetRegion("Dale"),
        ]);

        GetRegion("Erebor").AssignNeighbors([
            GetRegion("Dale"),
            GetRegion("IronHills"),
        ]);

        GetRegion("IronHills").AssignNeighbors([
            GetRegion("Dale"),
            GetRegion("Erebor"),
            GetRegion("ValeOfTheCarnen"),
            GetRegion("EastRhun"),
        ]);

        GetRegion("EastRhun").AssignNeighbors([
            GetRegion("NorthRhun"),
            GetRegion("ValeOfTheCarnen"),
            GetRegion("IronHills"),
        ]);

        GetRegion("NorthRhun").AssignNeighbors([
            GetRegion("EastRhun"),
            GetRegion("ValeOfTheCarnen"),
        ]);

        GetRegion("ValeOfTheCarnen").AssignNeighbors([
            GetRegion("Dale"),
            GetRegion("IronHills"),
            GetRegion("EastRhun"),
            GetRegion("NorthRhun"),
        ]);
    }
}
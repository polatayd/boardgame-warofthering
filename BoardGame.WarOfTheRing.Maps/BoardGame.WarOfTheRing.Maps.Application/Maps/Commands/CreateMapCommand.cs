using BoardGame.WarOfTheRing.Maps.Application.Maps.Inputs;
using BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps;
using BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps.Entities;
using BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps.ValueObjects;
using MediatR;
using Unit = BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps.ValueObjects.Unit;

namespace BoardGame.WarOfTheRing.Maps.Application.Maps.Commands;

public class CreateMapCommand : IRequest
{
    public CreateMapCommand(CreateMapCommandInput input)
    {
        Input = input;
    }

    public CreateMapCommandInput Input { get; set; }
}

public class CreateMapCommandHandler : IRequestHandler<CreateMapCommand>
{
    private readonly IMapRepository mapRepository;
    private readonly IUnitOfWork unitOfWork;

    public CreateMapCommandHandler(IMapRepository mapRepository, IUnitOfWork unitOfWork)
    {
        this.mapRepository = mapRepository;
        this.unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateMapCommand request, CancellationToken cancellationToken)
    {
        var map = new Map();

        var nations = CreateNations(request.Input.CreateNationsCommandInput, map.Id);
        var regions = CreateRegions(request.Input.CreateRegionsCommandInput, map.Id);

        map.WithNations(nations);
        map.WithRegions(regions);

        mapRepository.Add(map);

        await unitOfWork.SaveChangesAsync();
    }

    //TODO: Do this in factory. Eliminate magic strings like "Regular", "Elite", etc.
    private List<Nation> CreateNations(CreateNationsCommandInput createNationsCommandInput, Guid mapId)
    {
        var nations = new List<Nation>();

        foreach (var createNationInput in createNationsCommandInput.Nations)
        {
            var units = new List<Unit>();

            for (var i = 0; i < createNationInput.Reinforcements.RegularCount; i++)
            {
                units.Add(Unit.Create(createNationInput.Name, UnitType.FromValue("Regular")));
            }

            for (var i = 0; i < createNationInput.Reinforcements.EliteCount; i++)
            {
                units.Add(Unit.Create(createNationInput.Name, UnitType.FromValue("Elite")));
            }

            var nation = new Nation(createNationInput.Name, Force.FromName(createNationInput.BelongsTo), units, mapId);
            
            nations.Add(nation);
        }

        return nations;
    }

    private List<Region> CreateRegions(CreateRegionsCommandInput inputCreateRegionsCommandInput, Guid mapId)
    {
        var regions = new List<Region>();

        foreach (var createRegionInput in inputCreateRegionsCommandInput.Regions)
        {
            var neighbors = createRegionInput.NeighborRegions.Select(x => new Neighbor(x)).ToList();
            var terrain = CreateTerrain(createRegionInput.Terrain, createRegionInput.ControlledBy);
            var region = new Region(createRegionInput.Name, terrain, createRegionInput.InBorderOf, neighbors, mapId);
            regions.Add(region);
        }

        return regions;
    }

    private Terrain CreateTerrain(string terrain, string controlledBy)
    {
        if (terrain == "Empty")
        {
            return Terrain.CreateEmpty();
        }

        if (terrain == "Fortification")
        {
            return Terrain.CreateFortification();
        }

        if (terrain == "Town")
        {
            return Terrain.CreateSettlement(Settlement.Town(Force.FromName(controlledBy)));
        }

        if (terrain == "City")
        {
            return Terrain.CreateSettlement(Settlement.City(Force.FromName(controlledBy)));
        }

        if (terrain == "Stronghold")
        {
            return Terrain.CreateSettlement(Settlement.Stronghold(Force.FromName(controlledBy)));
        }

        throw new ArgumentException();
    }
}
using BoardGame.WarOfTheRing.Maps.Application.Maps.Inputs;
using BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps;
using BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps.Entities;
using BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps.Factories;
using MediatR;

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

    private List<Nation> CreateNations(CreateNationsCommandInput createNationsCommandInput, Guid mapId)
    {
        var nations = new List<Nation>();

        foreach (var createNationInput in createNationsCommandInput.Nations)
        {
            nations.Add(MapFactory.CreateNation(
                createNationInput.Name,
                createNationInput.BelongsTo,
                createNationInput.Reinforcements.RegularCount,
                createNationInput.Reinforcements.EliteCount,
                createNationInput.Reinforcements.LeaderCount,
                mapId));
        }

        return nations;
    }

    private List<Region> CreateRegions(CreateRegionsCommandInput inputCreateRegionsCommandInput, Guid mapId)
    {
        var regions = new List<Region>();

        foreach (var createRegionInput in inputCreateRegionsCommandInput.Regions)
        {
            regions.Add(MapFactory.CreateRegion(
                createRegionInput.Name,
                createRegionInput.Terrain,
                createRegionInput.ControlledBy,
                createRegionInput.InBorderOf,
                createRegionInput.NeighborRegions,
                createRegionInput.Army.Nation,
                createRegionInput.Army.RegularCount,
                createRegionInput.Army.EliteCount,
                createRegionInput.Army.LeaderCount,
                mapId));
        }

        return regions;
    }
}
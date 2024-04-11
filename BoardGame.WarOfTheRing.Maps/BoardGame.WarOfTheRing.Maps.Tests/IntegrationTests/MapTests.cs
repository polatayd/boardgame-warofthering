using BoardGame.WarOfTheRing.Maps.Api.EndpointHandlers;
using BoardGame.WarOfTheRing.Maps.Application;
using BoardGame.WarOfTheRing.Maps.Application.Maps;
using BoardGame.WarOfTheRing.Maps.Application.Maps.Commands;
using BoardGame.WarOfTheRing.Maps.Application.Maps.Inputs;
using BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps;
using BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps.Entities;
using BoardGame.WarOfTheRing.Maps.Domain.Aggregates.Maps.ValueObjects;

namespace BoardGame.WarOfTheRing.Maps.Tests.IntegrationTests;

public class MapTests
{
    [Fact]
    public async Task Map_Should_Be_Created_With_Correct_Neighbors()
    {
        //Arrange
        var map = await CreateMap();

        //Act

        //Assert
        foreach (var currentRegion in map.Regions)
        {
            foreach (var neighbor in currentRegion.Neighbors)
            {
                var neighborRegion = map.Regions.FirstOrDefault(x => x.Name == neighbor.Name);
                Assert.True(neighborRegion is not null, $"{neighbor.Name} is not found in regions");
                
                var condition = neighborRegion.Neighbors.Any(x => x.Name == currentRegion.Name);
                Assert.True(condition, $"{currentRegion.Name} is not found in {neighbor.Name}");
            }
        }
    }
    
    [Fact]
    public async Task Map_Should_Be_Created_With_Correct_Number_Of_Settlements()
    {
        //Arrange
        var map = await CreateMap();

        var fortificationCount = map.Regions.Count(x => x.Terrain.HasFortification);
        var townCount = map.Regions.Count(x => x.Terrain.Settlement.Type == SettlementTypes.Town);
        var cityCount = map.Regions.Count(x => x.Terrain.Settlement.Type == SettlementTypes.City);
        var strongholdCount = map.Regions.Count(x => x.Terrain.Settlement.Type == SettlementTypes.Stronghold);

        const int expectedFortificationCount = 2;
        const int expectedTownCount = 14;
        const int expectedCityCount = 6;
        const int expectedStrongholdCount = 16;
        
        //Act
        
        //Assert
        Assert.True(fortificationCount == expectedFortificationCount, $"Fortification count is {fortificationCount}, but it should be {expectedFortificationCount}");
        Assert.True(townCount == expectedTownCount, $"Town count is {townCount}, but it should be {expectedTownCount}");
        Assert.True(cityCount == expectedCityCount, $"City count is {cityCount}, but it should be {expectedCityCount}");
        Assert.True(strongholdCount == expectedStrongholdCount, $"Stronghold count is {strongholdCount}, but it should be {expectedStrongholdCount}");
    }
    
    [Fact]
    public async Task Map_Should_Be_Created_With_Correct_Number_Of_Regions_In_Border()
    {
        //Arrange
        var map = await CreateMap();

        var northRegionCount = map.Regions.Count(x => x.InBorderOf == NationNames.TheNorth);
        var elvesRegionCount = map.Regions.Count(x => x.InBorderOf == NationNames.Elves);
        var dwarvesRegionCount = map.Regions.Count(x => x.InBorderOf == NationNames.Dwarves);
        var rohanRegionCount = map.Regions.Count(x => x.InBorderOf == NationNames.Rohan);
        var gondorRegionCount = map.Regions.Count(x => x.InBorderOf == NationNames.Gondor);
        var isengardRegionCount = map.Regions.Count(x => x.InBorderOf == NationNames.Isengard);
        var southronsRegionCount = map.Regions.Count(x => x.InBorderOf == NationNames.Southrons);
        var sauronRegionCount = map.Regions.Count(x => x.InBorderOf == NationNames.Sauron);
        
        const int expectedNorthRegionCount = 8;
        const int expectedElvesRegionCount = 4;
        const int expectedDwarvesRegionCount = 4;
        const int expectedRohanRegionCount = 6;
        const int expectedGondorRegionCount = 8;
        const int expectedIsengardRegionCount = 4;
        const int expectedSouthronsRegionCount = 7;
        const int expectedSauronRegionCount = 11;
        
        //Act
        
        //Assert
        Assert.True(northRegionCount == expectedNorthRegionCount, $"TheNorth region count is {northRegionCount}, but it should be {expectedNorthRegionCount}");
        Assert.True(elvesRegionCount == expectedElvesRegionCount, $"Elves region count is {elvesRegionCount}, but it should be {expectedElvesRegionCount}");
        Assert.True(dwarvesRegionCount == expectedDwarvesRegionCount, $"Dwarves region count is {dwarvesRegionCount}, but it should be {expectedDwarvesRegionCount}");
        Assert.True(rohanRegionCount == expectedRohanRegionCount, $"Rohan region count is {rohanRegionCount}, but it should be {expectedRohanRegionCount}");
        Assert.True(gondorRegionCount == expectedGondorRegionCount, $"Gondor region count is {gondorRegionCount}, but it should be {expectedGondorRegionCount}");
        Assert.True(isengardRegionCount == expectedIsengardRegionCount, $"Isengard region count is {isengardRegionCount}, but it should be {expectedIsengardRegionCount}");
        Assert.True(southronsRegionCount == expectedSouthronsRegionCount, $"Southron region count is {southronsRegionCount}, but it should be {expectedSouthronsRegionCount}");
        Assert.True(sauronRegionCount == expectedSauronRegionCount, $"Sauron region count is {sauronRegionCount}, but it should be {expectedSauronRegionCount}");
    }

    private static async Task<Map> CreateMap()
    {
        var createMapCommand = new CreateMapCommand(new CreateMapCommandInput(
            await MapHandlers.GetCreateNationsCommandInputFromFile(),
            await MapHandlers.GetCreateRegionsCommandInputFromFile()
        ));

        var mapTestRepository = new MapTestRepository();
        var createMapCommandHandler = new CreateMapCommandHandler(
            mapTestRepository,
            new MapTestUnitOfWork());

        await createMapCommandHandler.Handle(createMapCommand, CancellationToken.None);
        return mapTestRepository.Get();
    }

    private class MapTestRepository : IMapRepository
    {
        private Map map;
        
        public void Add(Map map)
        {
            this.map = map;
        }

        public Map Get()
        {
            return map;
        }
    }

    //TODO: Do this with Mock library.
    private class MapTestUnitOfWork : IUnitOfWork
    {
        public void Dispose()
        {
        }

        public Task<int> SaveChangesAsync()
        {
            return Task.FromResult(0);
        }
    }
}
using System.Net.Http.Headers;
using System.Net.Http.Json;
using BoardGame.WarOfTheRing.Fellowships.Application.Services;
using BoardGame.WarOfTheRing.Fellowships.Domain.Aggregates.Fellowships.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BoardGame.WarOfTheRing.Fellowships.Infrastructure.Services.Map;

public class MapApiClient : IMapService
{
    private readonly HttpClient client;
    private readonly JsonSerializerOptionsWrapper jsonSerializerOptionsWrapper;
    private readonly ILogger<MapApiClient> logger;

    public MapApiClient(HttpClient client, JsonSerializerOptionsWrapper jsonSerializerOptionsWrapper,
        ILogger<MapApiClient> logger)
    {
        this.client = client;
        this.jsonSerializerOptionsWrapper = jsonSerializerOptionsWrapper;
        this.logger = logger;
    }

    //TODO: This service should return Region that contains fellowship and this service should decide that it's available for reroll or not.
    public async Task<int> SendReRollCountRequestAsync(Guid gameId)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"fellowships/{gameId}/reroll-availability");

        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        try
        {
            var response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                var errorAsProblemDetails = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>(jsonSerializerOptionsWrapper.Options);

                errorAsProblemDetails.LogValidationProblemDetails(logger);

                throw new MapServiceException($"Map services returned error with status code {response.StatusCode}");
            }

            var output = await response.Content.ReadFromJsonAsync<RerollIsAvailableRequestOutput>(jsonSerializerOptionsWrapper.Options);

            return output.ReRollCount;
        }
        catch (OperationCanceledException e)
        {
            logger.LogError("Message:{Message}", e.Message);
            throw new MapServiceException($"Map service operation canceled");
        }
    }

    public async Task<Region> SendMoveFellowshipRequestAsync(Guid gameId)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, $"fellowships/{gameId}/movement");

        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        try
        {
            var response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                var errorAsProblemDetails = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>(jsonSerializerOptionsWrapper.Options);

                errorAsProblemDetails.LogValidationProblemDetails(logger);

                throw new MapServiceException($"Map services returned error with status code {response.StatusCode}");
            }

            //TODO: This information should come from map service.
            return new Region(RegionPlayer.FreePeoples, RegionPlayer.FreePeoples, RegionType.City);
        }
        catch (OperationCanceledException e)
        {
            logger.LogError("Message:{Message}", e.Message);
            throw new MapServiceException($"Map service operation canceled");
        }
    }
}
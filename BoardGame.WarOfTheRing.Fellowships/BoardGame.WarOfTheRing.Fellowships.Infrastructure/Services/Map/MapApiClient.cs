using System.Net.Http.Headers;
using System.Text.Json;
using BoardGame.WarOfTheRing.Fellowships.Application.Services;
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
                var errorContent = await response.Content.ReadAsStringAsync();
                var errorAsProblemDetails =
                    JsonSerializer.Deserialize<ValidationProblemDetails>(errorContent,
                        jsonSerializerOptionsWrapper.Options);

                errorAsProblemDetails.LogValidationProblemDetails(logger);

                throw new MapServiceException($"Map services returned error with status code {response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();
            var output =
                JsonSerializer.Deserialize<RerollIsAvailableRequestOutput>(content,
                    jsonSerializerOptionsWrapper.Options);

            return output.ReRollCount;
        }
        catch (OperationCanceledException e)
        {
            logger.LogError("Message:{Message}", e.Message);
            throw new MapServiceException($"Map service operation canceled");
        }
    }
}
using System.Net.Http.Headers;
using System.Text.Json;
using BoardGame.WarOfTheRing.Fellowships.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BoardGame.WarOfTheRing.Fellowships.Infrastructure.Services.Dice;

public class DiceApiClient : IDiceService
{
    private readonly HttpClient client;
    private readonly JsonSerializerOptionsWrapper jsonSerializerOptionsWrapper;
    private readonly ILogger<DiceApiClient> logger;

    public DiceApiClient(HttpClient client, JsonSerializerOptionsWrapper jsonSerializerOptionsWrapper,
        ILogger<DiceApiClient> logger)
    {
        this.client = client;
        this.jsonSerializerOptionsWrapper = jsonSerializerOptionsWrapper;
        this.logger = logger;
    }

    public async Task<List<int>> SendRollDiceRequestAsync(int numberOfDice)
    {
        var input = new RollDiceRequestInput { NumberOfDice = numberOfDice };
        var serializedInput = JsonSerializer.Serialize(input, jsonSerializerOptionsWrapper.Options);

        var request = new HttpRequestMessage(HttpMethod.Post, "dicepool");

        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        request.Content = new StringContent(serializedInput);
        request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

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

                throw new DiceServiceException($"Dice services returned error with status code {response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();
            var output =
                JsonSerializer.Deserialize<RollDiceRequestOutput>(content, jsonSerializerOptionsWrapper.Options);

            return output.Results.Select(x => x.Value).ToList();
        }
        catch (OperationCanceledException e)
        {
            logger.LogError("Message:{Message}", e.Message);
            throw new DiceServiceException($"Dice service operation canceled");
        }
    }
}
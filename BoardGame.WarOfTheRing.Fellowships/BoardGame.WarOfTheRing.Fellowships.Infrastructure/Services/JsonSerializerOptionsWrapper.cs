using System.Text.Json;

namespace BoardGame.WarOfTheRing.Fellowships.Infrastructure.Services;

public class JsonSerializerOptionsWrapper
{
    public JsonSerializerOptionsWrapper()
    {
        Options = new JsonSerializerOptions(JsonSerializerDefaults.Web);
    }

    public JsonSerializerOptions Options { get; }
}
using System.Text.Json.Serialization;
using BoardGame.WarOfTheRing.Fellowships.Application.Fellowships.IntegrationEvents;

namespace BoardGame.WarOfTheRing.Fellowships.Application;

[JsonDerivedType(typeof(FellowshipDeclaredIntegrationEvent),
    typeDiscriminator: nameof(FellowshipDeclaredIntegrationEvent))]
public abstract class IntegrationEvent
{
    public DateTime DateOccurred { get; protected set; } = DateTime.UtcNow;
}
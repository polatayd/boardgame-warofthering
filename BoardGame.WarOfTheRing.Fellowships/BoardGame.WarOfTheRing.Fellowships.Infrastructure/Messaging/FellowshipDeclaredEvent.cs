

// ReSharper disable once CheckNamespace
namespace BoardGame.WarOfTheRing.Infrastructure.Messaging.Events;

public class FellowshipDeclaredEvent : IIntegrationEvent
{
    public Guid GameId { get; set; }
    public DateTime OccurredOn { get; set; }
    public string NationName { get; set; }
}
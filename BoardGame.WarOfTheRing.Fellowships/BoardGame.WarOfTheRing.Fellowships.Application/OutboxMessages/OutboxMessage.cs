using System.Text.Json;

namespace BoardGame.WarOfTheRing.Fellowships.Application.OutboxMessages;

public class OutboxMessage
{
    public Guid Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime OccuredOn { get; set; }
    public DateTime? ProcessedOn { get; set; }
    public string Error { get; set; }
}

public static class IntegrationEventExtensions
{
    public static OutboxMessage ToOutboxMessage(this IntegrationEvent integrationEvent)
    {
        if (integrationEvent is null)
        {
            return null;
        }
        
        return new OutboxMessage()
        {
            Id = Guid.NewGuid(),
            OccuredOn = DateTime.UtcNow,
            Type = integrationEvent.GetType().ToString(),
            Content = JsonSerializer.Serialize(integrationEvent)
        };
    }
}
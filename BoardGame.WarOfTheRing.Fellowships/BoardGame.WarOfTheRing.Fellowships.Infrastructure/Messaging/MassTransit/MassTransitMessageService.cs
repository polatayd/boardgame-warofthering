using BoardGame.WarOfTheRing.Fellowships.Application;
using BoardGame.WarOfTheRing.Fellowships.Application.Services;
using MassTransit;

namespace BoardGame.WarOfTheRing.Fellowships.Infrastructure.Messaging.MassTransit;

public class MassTransitMessageService : IMessageService
{
    private readonly IPublishEndpoint publishEndpoint;
    private readonly TimeSpan timeout = TimeSpan.FromSeconds(1);

    public MassTransitMessageService(IPublishEndpoint publishEndpoint)
    {
        this.publishEndpoint = publishEndpoint;
    }

    public async Task SendAsync(IntegrationEvent integrationEvent)
    {
        var massTransitEvent = EventMapper.MapEvent(integrationEvent);
        
        using var source = new CancellationTokenSource(timeout);
        await publishEndpoint.Publish((dynamic)massTransitEvent, source.Token);
    }
}
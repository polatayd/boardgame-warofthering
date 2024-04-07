using BoardGame.WarOfTheRing.Fellowships.Application;
using BoardGame.WarOfTheRing.Fellowships.Application.Services;
using NServiceBus;

namespace BoardGame.WarOfTheRing.Fellowships.Infrastructure.Messaging.NServiceBus;

public class NServiceBusMessageService : IMessageService
{
    private readonly IMessageSession messageSession;

    public NServiceBusMessageService(IMessageSession messageSession)
    {
        this.messageSession = messageSession;
    }

    public async Task SendAsync(IntegrationEvent integrationEvent)
    {
        var nservicebusEvent = EventMapper.MapEvent(integrationEvent);
        
        await messageSession.Publish(nservicebusEvent);
    }
}
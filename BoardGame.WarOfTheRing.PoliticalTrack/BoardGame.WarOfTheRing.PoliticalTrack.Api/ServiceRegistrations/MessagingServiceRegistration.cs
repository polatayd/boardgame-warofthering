using BoardGame.WarOfTheRing.PoliticalTrack.Infrastructure.Messaging.MassTransit;
using MassTransit;
using NServiceBus;

namespace BoardGame.WarOfTheRing.PoliticalTrack.Api.ServiceRegistrations;

public static class MessagingServiceRegistration
{
    public static WebApplicationBuilder RegisterMessagingServices(this WebApplicationBuilder builder,
        IConfiguration configuration)
    {
        var messagingOptions = configuration.GetSection(MessagingOptions.Messaging).Get<MessagingOptions>();

        if (messagingOptions.NServiceBus.Enabled)
        {
            builder.Host.UseNServiceBus(_ =>
            {
                var endpointConfiguration = new EndpointConfiguration("Fellowship.Integration");

                var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
                transport.ConnectionString(messagingOptions.NServiceBus.ConnectionString);
                transport.UseConventionalRoutingTopology(QueueType.Quorum);

                endpointConfiguration.SendOnly();
                endpointConfiguration.EnableInstallers();
                return endpointConfiguration;
            });
        }
        else if (messagingOptions.MassTransit.Enabled)
        {
            builder.Services.AddMassTransit(x =>
            {
                x.AddConsumer<FellowshipDeclaredMassTransitEventHandler>();
                
                x.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host(messagingOptions.MassTransit.Host);
        
                    configurator.ConfigureEndpoints(context);
                });
            });
        }

        return builder;
    }
}
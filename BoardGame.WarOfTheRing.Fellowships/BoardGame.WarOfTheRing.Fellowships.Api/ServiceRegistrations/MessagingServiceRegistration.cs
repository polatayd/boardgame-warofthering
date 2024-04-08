using BoardGame.WarOfTheRing.Fellowships.Application.Services;
using BoardGame.WarOfTheRing.Fellowships.Infrastructure.Messaging.MassTransit;
using BoardGame.WarOfTheRing.Fellowships.Infrastructure.Messaging.NServiceBus;
using MassTransit;
using NServiceBus;

namespace BoardGame.WarOfTheRing.Fellowships.Api.ServiceRegistrations;

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
            
            builder.Services.AddSingleton<IMessageService, NServiceBusMessageService>();
        }
        else if (messagingOptions.MassTransit.Enabled)
        {
            builder.Services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host(messagingOptions.MassTransit.Host);
        
                    configurator.ConfigureEndpoints(context);
                });
            });
            builder.Services.AddScoped<IMessageService, MassTransitMessageService>();
        }

        return builder;
    }
}
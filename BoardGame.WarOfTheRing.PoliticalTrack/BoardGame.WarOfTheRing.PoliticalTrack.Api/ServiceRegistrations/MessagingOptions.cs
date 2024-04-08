namespace BoardGame.WarOfTheRing.PoliticalTrack.Api.ServiceRegistrations;

public class MessagingOptions
{
    public const string Messaging = "Messaging";
    
    public NServiceBusOptions NServiceBus { get; set; }
    public MassTransitOptions MassTransit { get; set; }
}

public class NServiceBusOptions
{
    public bool Enabled { get; set; }
    public string ConnectionString { get; set; }
}

public class MassTransitOptions
{
    public bool Enabled { get; set; }
    public string Host { get; set; }
}
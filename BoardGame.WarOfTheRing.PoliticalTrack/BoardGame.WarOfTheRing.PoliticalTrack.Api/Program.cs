using BoardGame.WarOfTheRing.PoliticalTrack.Api;
using BoardGame.WarOfTheRing.PoliticalTrack.Api.EndpointMappings;
using BoardGame.WarOfTheRing.PoliticalTrack.Api.ServiceRegistrations;
using NServiceBus;
using Serilog;
using Serilog.Formatting.Elasticsearch;

var logger = new LoggerConfiguration()
    .WriteTo.Console(new ElasticsearchJsonFormatter())
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Host.UseSerilog(logger);

builder.Host.UseNServiceBus(_ =>
{
    var endpointConfiguration = new EndpointConfiguration("PoliticalTrack.Integration");

    var transport = endpointConfiguration.UseTransport<RabbitMQTransport>();
    transport.ConnectionString(builder.Configuration.GetConnectionString("rabbitmq"));
    transport.UseConventionalRoutingTopology(QueueType.Quorum);

    endpointConfiguration.EnableInstallers();
    return endpointConfiguration;
});

builder.Services.RegisterPoliticalTrackServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.ApplicationIsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.ApplicationIsDevelopment())
{
    app.UseExceptionHandler();
}

app.UseStatusCodePages();

app.UseHttpsRedirection();

app.RegisterNationEndpoints();

app.Run();
using Microsoft.Extensions.Logging;

namespace BoardGame.WarOfTheRing.Fellowships.Infrastructure.Services;

public class LoggingDelegatingHandler : DelegatingHandler
{
    private readonly ILogger<LoggingDelegatingHandler> logger;

    public LoggingDelegatingHandler(ILogger<LoggingDelegatingHandler> logger)
    {
        this.logger = logger;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (request.Content is not null)
        {
            logger.LogInformation("Request:{Request}", await request.Content.ReadAsStringAsync(cancellationToken));
        }
        
        var response = await base.SendAsync(request, cancellationToken);
        
        logger.LogInformation("Response:{Response}", await response.Content.ReadAsStringAsync(cancellationToken));
        return response;
    }
}
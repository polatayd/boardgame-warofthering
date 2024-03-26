namespace BoardGame.WarOfTheRing.PoliticalTrack.Api;

public static class ApplicationEnvironments
{
    public static readonly string DockerDevelopment = nameof (DockerDevelopment);
    
    public static bool ApplicationIsDevelopment(this IWebHostEnvironment environment)
    {
        return environment.IsDevelopment() ||
               environment.IsEnvironment(nameof(ApplicationEnvironments.DockerDevelopment));
    }
}
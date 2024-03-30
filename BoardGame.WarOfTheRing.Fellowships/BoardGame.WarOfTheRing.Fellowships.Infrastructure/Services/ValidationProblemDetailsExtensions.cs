using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BoardGame.WarOfTheRing.Fellowships.Infrastructure.Services;

public static class ValidationProblemDetailsExtensions
{
    public static void LogValidationProblemDetails(this ValidationProblemDetails validationProblemDetails, ILogger logger)
    {
        if (validationProblemDetails.Detail is not null)
        {
            logger.LogError("Message:{Message}", validationProblemDetails.Detail);
        }

        if (validationProblemDetails.Errors.Count <= 0) 
            return;
        
        foreach (var error in validationProblemDetails.Errors)
        {
            foreach (var errorValue in error.Value)
            {
                logger.LogError("Validation Error Occured Key:{Key} Value:{Value}", error.Key, errorValue);
            }
        }
    }
}
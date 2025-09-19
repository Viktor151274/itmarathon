using Epam.ItMarathon.ApiService.Domain.Shared.ValidationErrors;
using FluentValidation.Results;

namespace Epam.ItMarathon.ApiService.Api.Endpoints.Extension;

internal static class ValidationExtensions
{
    internal static IResult ValidationProblem(this ValidationResult error)
    {
        return Results.ValidationProblem(error.ToDictionary(), statusCode: error.GetStatusCode());
    }

    private static int GetStatusCode(this ValidationResult error)
    {
        return error switch
        {
            NotAuthorizedError => StatusCodes.Status401Unauthorized,
            NotFoundError => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status400BadRequest,
        };
    }
}
﻿using Epam.ItMarathon.ApiService.Domain.Shared.ValidationErrors;
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
            BadRequestError => StatusCodes.Status400BadRequest,
            NotAuthorizedError => StatusCodes.Status401Unauthorized,
            ForbiddenError => StatusCodes.Status403Forbidden,
            NotFoundError => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status400BadRequest,
        };
    }
}
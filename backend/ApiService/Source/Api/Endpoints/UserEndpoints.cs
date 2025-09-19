using AutoMapper;
using Epam.ItMarathon.ApiService.Api.Dto.CreationDtos;
using Epam.ItMarathon.ApiService.Api.Endpoints.Extension;
using Epam.ItMarathon.ApiService.Api.Endpoints.Extension.SwaggerTagExtension;
using Epam.ItMarathon.ApiService.Api.Filters.Validation;
using Epam.ItMarathon.ApiService.Application.UseCases.UserCases.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Epam.ItMarathon.ApiService.Api.Endpoints
{
    public static class UserEndpoints
    {
        public static WebApplication MapUserEndpoints(this WebApplication app)
        {
            var root = app.MapGroup("/api/users")
                .WithTags("User")
                .WithTagDescription("User", "User endpoints")
                .WithOpenApi();

            _ = root.MapGet("", GetUsersRequest)
                .AddEndpointFilterFactory(ValidationFactoryFilter.GetValidationFactory)
                .Produces<List<UserReadDto>>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithSummary("Auth by UserCode and Read all user in auth user's room.")
                .WithDescription("Return list of users.");

            _ = root.MapGet("{id:long}", GetUserWithIdRequest)
                .AddEndpointFilterFactory(ValidationFactoryFilter.GetValidationFactory)
                .Produces<List<UserReadDto>>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status401Unauthorized)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithSummary("Auth by UserCode and Read user info by user Id.")
                .WithDescription("Return user info.");

            return app;
        }

        public static async Task<IResult> GetUsersRequest([FromQuery][Validate] string? userCode, IMediator mediator, IMapper mapper)
        {
            var result = await mediator.Send(new GetUsersQuery(userCode!, null));
            if (result.IsFailure)
            {
                return result.Error.ValidationProblem();
            }

            var responseUsers = mapper.Map<List<UserReadDto>>(result.Value,
                options => { options.SetUserMappingOptions(result.Value, userCode!); });
            return Results.Ok(responseUsers);
        }

        public static async Task<IResult> GetUserWithIdRequest([FromRoute] ulong id, [FromQuery][Validate] string? userCode, IMediator mediator, IMapper mapper)
        {
            var result = await mediator.Send(new GetUsersQuery(userCode!, id));
            if (result.IsFailure)
            {
                return result.Error.ValidationProblem();
            }

            var responseUser = mapper.Map<List<UserReadDto>>(new[] { result.Value.First(user => user.Id.Equals(id)) },
                options => { options.SetUserMappingOptions(result.Value, userCode!); });
            return Results.Ok(responseUser);
        }
    }
}
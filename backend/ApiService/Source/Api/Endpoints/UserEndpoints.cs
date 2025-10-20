using AutoMapper;
using Epam.ItMarathon.ApiService.Api.Dto.CreationDtos;
using Epam.ItMarathon.ApiService.Api.Dto.Requests.UserRequests;
using Epam.ItMarathon.ApiService.Api.Dto.Responses.UserResponses;
using Epam.ItMarathon.ApiService.Api.Endpoints.Extension;
using Epam.ItMarathon.ApiService.Api.Endpoints.Extension.SwaggerTagExtension;
using Epam.ItMarathon.ApiService.Api.Filters.Validation;
using Epam.ItMarathon.ApiService.Application.Models.Creation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Epam.ItMarathon.ApiService.Application.UseCases.User.Commands;
using Epam.ItMarathon.ApiService.Application.UseCases.User.Queries;

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

            _ = root.MapGet("", GetUsers)
                .AddEndpointFilterFactory(ValidationFactoryFilter.GetValidationFactory)
                .Produces<List<UserReadDto>>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithSummary("Auth by UserCode and Read all user in auth user's room.")
                .WithDescription("Return list of users.");

            _ = root.MapGet("{id:long}", GetUserWithId)
                .AddEndpointFilterFactory(ValidationFactoryFilter.GetValidationFactory)
                .Produces<List<UserReadDto>>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status401Unauthorized)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithSummary("Auth by UserCode and Read user info by user Id.")
                .WithDescription("Return user info.");

            _ = root.MapPost("", JoinUserToRoom)
                .Produces<UserCreationResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithOpenApi(operation =>
                {
                    operation.Responses.Remove(StatusCodes.Status200OK.ToString());
                    return operation;
                })
                .WithSummary("Create and add user to a room.")
                .WithDescription("Return created user info.");

            return app;
        }

        public static async Task<IResult> GetUsers([FromQuery, Required] string? userCode, IMediator mediator,
            IMapper mapper, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetUsersQuery(userCode!, null), cancellationToken);
            if (result.IsFailure)
            {
                return result.Error.ValidationProblem();
            }

            var responseUsers = mapper.Map<List<UserReadDto>>(result.Value,
                options => { options.SetUserMappingOptions(result.Value, userCode!); });
            return Results.Ok(responseUsers);
        }

        public static async Task<IResult> GetUserWithId([FromRoute] ulong id, [FromQuery, Required] string? userCode,
            IMediator mediator, IMapper mapper, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetUsersQuery(userCode!, id), cancellationToken);
            if (result.IsFailure)
            {
                return result.Error.ValidationProblem();
            }

            var responseUser = mapper.Map<List<UserReadDto>>(new[] { result.Value.First(user => user.Id.Equals(id)) },
                options => { options.SetUserMappingOptions(result.Value, userCode!); });
            return Results.Ok(responseUser);
        }

        public static async Task<IResult> JoinUserToRoom([FromQuery, Required] string roomCode,
            UserCreationRequest user, IMediator mediator, IMapper mapper, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new CreateUserInRoomRequest(
                mapper.Map<UserApplication>(user), roomCode), cancellationToken);
            return result.IsFailure
                ? result.Error.ValidationProblem()
                : Results.Created(string.Empty, mapper.Map<UserCreationResponse>(result.Value));
        }
    }
}
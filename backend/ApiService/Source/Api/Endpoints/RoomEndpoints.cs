using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Epam.ItMarathon.ApiService.Api.Dto.CreationDtos;
using Epam.ItMarathon.ApiService.Api.Dto.ReadDtos;
using Epam.ItMarathon.ApiService.Api.Dto.Requests.RoomRequests;
using Epam.ItMarathon.ApiService.Api.Dto.Responses.RoomResponses;
using Epam.ItMarathon.ApiService.Api.Endpoints.Extension;
using Epam.ItMarathon.ApiService.Api.Endpoints.Extension.SwaggerTagExtension;
using Epam.ItMarathon.ApiService.Api.Filters.Validation;
using Epam.ItMarathon.ApiService.Application.Models.Creation;
using Epam.ItMarathon.ApiService.Application.UseCases.RoomCases.Commands;
using Epam.ItMarathon.ApiService.Application.UseCases.RoomCases.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Epam.ItMarathon.ApiService.Api.Endpoints
{
    public static class RoomEndpoints
    {
        public static WebApplication MapRoomEndpoints(this WebApplication app)
        {
            var root = app.MapGroup("/api/rooms")
                .WithTags("Room")
                .WithTagDescription("Room", "Room endpoints")
                .WithOpenApi();

            _ = root.MapPost("", CreateRoomRequest)
                .AddEndpointFilterFactory(ValidationFactoryFilter.GetValidationFactory)
                .Produces<RoomCreationResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithOpenApi(operation =>
                {
                    operation.Responses.Remove(StatusCodes.Status200OK.ToString());
                    return operation;
                })
                .WithSummary("Create room for prize draw.")
                .WithDescription("Return created room info.");

            _ = root.MapGet("", GetRoomRequest)
                .AddEndpointFilterFactory(ValidationFactoryFilter.GetValidationFactory)
                .Produces<RoomReadDto>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithSummary("Read room info by User code or Room code.")
                .WithDescription("Return room info.");

            _ = root.MapPost("draw", DrawRoomRequest)
                .AddEndpointFilterFactory(ValidationFactoryFilter.GetValidationFactory)
                .Produces<UserReadDto>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status403Forbidden)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithSummary("Get room by User code and draw it.")
                .WithDescription("Return admin's gift recipient info");

            return app;
        }

        public static Task<IResult> CreateRoomRequest([Validate] RoomCreationRequest request, IMediator mediator, IMapper mapper)
        {
            var result = mediator.Send(new CreateRoomCommand(mapper.Map<RoomApplication>(request.Room),
                mapper.Map<UserApplication>(request.AdminUser))).Result;
            if (result.IsFailure)
            {
                return Task.FromResult(result.Error.ValidationProblem());
            }

            return Task.FromResult(Results.Created(string.Empty, new RoomCreationResponse()
            {
                Room = mapper.Map<RoomReadDto>(result.Value),
                UserCode = result.Value.Users.Where(user => user.IsAdmin).First().AuthCode
            }));
        }

        public static Task<IResult> GetRoomRequest([AsParameters][Validate] RoomReadingRequest request, IMediator mediator, IMapper mapper)
        {
            var result = mediator.Send(new GetRoomQuery(request.UserCode, request.RoomCode)).Result;
            if (result.IsFailure)
            {
                return Task.FromResult(result.Error.ValidationProblem());
            }

            return Task.FromResult(Results.Ok(mapper.Map<RoomReadDto>(result.Value)));
        }

        public static Task<IResult> DrawRoomRequest([FromQuery, Required] string? userCode, IMediator mediator, IMapper mapper)
        {
            var result = mediator.Send(new DrawRoomCommand(userCode!)).Result;
            if (result.IsFailure)
            {
                return Task.FromResult(result.Error.ValidationProblem());
            }

            var responseUsers = result.Value;
            var adminUser = responseUsers.First(user => user.AuthCode.Equals(userCode));
            var responseUser = mapper.Map<UserReadDto>(responseUsers.First(user => user.Id.Equals(adminUser.GiftToUserId)),
                options => { options.SetUserMappingOptions(responseUsers, userCode!); });
            return Task.FromResult(Results.Ok(responseUser));
        }
    }
}

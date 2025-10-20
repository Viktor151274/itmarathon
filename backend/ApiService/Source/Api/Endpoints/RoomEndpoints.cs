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
using Epam.ItMarathon.ApiService.Application.UseCases.Room.Commands;
using Epam.ItMarathon.ApiService.Application.UseCases.Room.Queries;
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

            _ = root.MapPost("", CreateRoom)
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

            _ = root.MapGet("", GetRoom)
                .AddEndpointFilterFactory(ValidationFactoryFilter.GetValidationFactory)
                .Produces<RoomReadDto>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithSummary("Read room info by User code or Room code.")
                .WithDescription("Return room info.");

            _ = root.MapPost("draw", DrawRoom)
                .AddEndpointFilterFactory(ValidationFactoryFilter.GetValidationFactory)
                .Produces<UserReadDto>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status403Forbidden)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithSummary("Get room by User code and draw it.")
                .WithDescription("Return admin's gift recipient info.");

            _ = root.MapPatch("", UpdateRoom)
                .AddEndpointFilterFactory(ValidationFactoryFilter.GetValidationFactory)
                .Produces<RoomReadDto>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status403Forbidden)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithSummary("Get room by User code and patch update it.")
                .WithDescription("Return updated room info.");

            return app;
        }

        public static async Task<IResult> CreateRoom([Validate] RoomCreationRequest request, IMediator mediator,
            IMapper mapper, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new CreateRoomCommand(mapper.Map<RoomApplication>(request.Room),
                mapper.Map<UserApplication>(request.AdminUser)), cancellationToken);
            if (result.IsFailure)
            {
                return result.Error.ValidationProblem();
            }

            return Results.Created(string.Empty, new RoomCreationResponse()
            {
                Room = mapper.Map<RoomReadDto>(result.Value),
                UserCode = result.Value.Users.First(user => user.IsAdmin).AuthCode
            });
        }

        public static async Task<IResult> GetRoom([AsParameters] [Validate] RoomReadingRequest request,
            IMediator mediator, IMapper mapper, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetRoomQuery(request.UserCode, request.RoomCode), cancellationToken);

            return result.IsFailure
                ? result.Error.ValidationProblem()
                : Results.Ok(mapper.Map<RoomReadDto>(result.Value));
        }

        public static async Task<IResult> DrawRoom([FromQuery, Required] string? userCode, IMediator mediator,
            IMapper mapper, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new DrawRoomCommand(userCode!), cancellationToken);
            if (result.IsFailure)
            {
                return result.Error.ValidationProblem();
            }

            var responseUsers = result.Value;
            var adminUser = responseUsers.First(user => user.AuthCode.Equals(userCode));
            var responseUser = mapper.Map<UserReadDto>(
                responseUsers.First(user => user.Id.Equals(adminUser.GiftRecipientUserId)),
                options => { options.SetUserMappingOptions(responseUsers, userCode!); });
            return Results.Ok(responseUser);
        }

        public static async Task<IResult> UpdateRoom([FromQuery, Required] string userCode,
            [FromBody] RoomPatchRequest patchRequest, IMediator mediator, IMapper mapper,
            CancellationToken cancellationToken)
        {
            var result = await mediator.Send(
                new UpdateRoomCommand(userCode, patchRequest.Name, patchRequest.Description,
                    patchRequest.InvitationNote, patchRequest.GiftExchangeDate, patchRequest.GiftMaximumBudget),
                cancellationToken);

            return result.IsFailure
                ? result.Error.ValidationProblem()
                : Results.Ok(mapper.Map<RoomReadDto>(result.Value));
        }
    }
}
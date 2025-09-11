using AutoMapper;
using Epam.ItMarathon.ApiService.Api.Dto.Requests.RoomRequests;
using Epam.ItMarathon.ApiService.Api.Endpoints.Extension.SwaggerTagExtension;
using Epam.ItMarathon.ApiService.Api.Filters.Validation;
using Epam.ItMarathon.ApiService.Application.Models.Creation;
using Epam.ItMarathon.ApiService.Application.UseCases.RoomCases.Commands;
using MediatR;

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
                .Produces<string>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status500InternalServerError)
                .WithSummary("Create room for prize draw.")
                .WithDescription("Return created room info.");

            return app;
        }

        public static Task<IResult> CreateRoomRequest([Validate] RoomCreationRequest request, IMediator mediator, IMapper mapper)
        {
            var result = mediator.Send(new CreateRoomCommand(mapper.Map<RoomApplication>(request.Room), 
                mapper.Map<UserApplication>(request.Admin))).Result;
            if (result.IsSuccess) 
                return Task.FromResult(Results.Ok(result.Value));
            return Task.FromResult(Results.ValidationProblem(result.Error.ToDictionary()));
        }
    }
}

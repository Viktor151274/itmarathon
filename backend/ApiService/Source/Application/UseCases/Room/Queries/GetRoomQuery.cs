using CSharpFunctionalExtensions;
using FluentValidation.Results;
using MediatR;
using RoomAggregate = Epam.ItMarathon.ApiService.Domain.Aggregate.Room.Room;

namespace Epam.ItMarathon.ApiService.Application.UseCases.Room.Queries
{
    public record GetRoomQuery(string? UserCode, string? RoomCode)
        : IRequest<Result<RoomAggregate, ValidationResult>>;
}
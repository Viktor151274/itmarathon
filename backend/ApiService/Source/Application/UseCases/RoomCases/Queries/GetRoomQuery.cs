using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Domain.Aggregate.Room;
using FluentValidation.Results;
using MediatR;

namespace Epam.ItMarathon.ApiService.Application.UseCases.RoomCases.Queries
{
    public record GetRoomQuery(string? UserCode, string? RoomCode) : IRequest<Result<Room, ValidationResult>>;
}
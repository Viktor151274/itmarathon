using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Application.Models.Creation;
using FluentValidation.Results;
using MediatR;
using RoomAggregate = Epam.ItMarathon.ApiService.Domain.Aggregate.Room.Room;

namespace Epam.ItMarathon.ApiService.Application.UseCases.Room.Commands
{
    public record CreateRoomCommand(RoomApplication Room, UserApplication Admin)
        : IRequest<Result<RoomAggregate, ValidationResult>>;
}

using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Application.Models.Creation;
using Epam.ItMarathon.ApiService.Domain.Aggregate.Room;
using FluentValidation.Results;
using MediatR;

namespace Epam.ItMarathon.ApiService.Application.UseCases.RoomCases.Commands
{
    public record CreateRoomCommand(RoomApplication Room, UserApplication Admin) : IRequest<Result<Room, ValidationResult>>
    {
    }
}

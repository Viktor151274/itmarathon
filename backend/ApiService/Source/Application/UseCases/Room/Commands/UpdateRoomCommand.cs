using CSharpFunctionalExtensions;
using FluentValidation.Results;
using MediatR;
using RoomAggregate = Epam.ItMarathon.ApiService.Domain.Aggregate.Room.Room;

namespace Epam.ItMarathon.ApiService.Application.UseCases.Room.Commands
{
    public record UpdateRoomCommand(
        string UserCode,
        string? Name,
        string? Description,
        string? InvitationNote,
        DateTime? GiftExchangeDate,
        ulong? GiftMaximumBudget) : IRequest<Result<RoomAggregate, ValidationResult>>;
}
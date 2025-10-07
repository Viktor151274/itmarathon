using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Domain.Aggregate.Room;
using FluentValidation.Results;
using MediatR;

namespace Epam.ItMarathon.ApiService.Application.UseCases.RoomCases.Commands
{
    public record UpdateRoomCommand(
        string UserCode,
        string? Name,
        string? Description,
        string? InvitationNote,
        DateTime? GiftExchangeDate,
        ulong? GiftMaximumBudget) : IRequest<Result<Room, ValidationResult>>;
}
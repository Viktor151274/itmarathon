using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Domain.Entities.User;
using FluentValidation.Results;
using MediatR;

namespace Epam.ItMarathon.ApiService.Application.UseCases.RoomCases.Commands
{
    public record DrawRoomCommand(string UserCode) : IRequest<Result<List<User>, ValidationResult>>;
}
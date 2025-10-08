using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Application.Models.Creation;
using Epam.ItMarathon.ApiService.Domain.Entities.User;
using FluentValidation.Results;
using MediatR;

namespace Epam.ItMarathon.ApiService.Application.UseCases.UserCases.Commands
{
    public record CreateUserInRoomRequest(UserApplication User, string RoomCode)
        : IRequest<IResult<User, ValidationResult>>;
}
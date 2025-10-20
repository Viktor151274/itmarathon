using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Application.Models.Creation;
using FluentValidation.Results;
using MediatR;
using UserEntity = Epam.ItMarathon.ApiService.Domain.Entities.User.User;

namespace Epam.ItMarathon.ApiService.Application.UseCases.User.Commands
{
    public record CreateUserInRoomRequest(UserApplication User, string RoomCode)
        : IRequest<IResult<UserEntity, ValidationResult>>;
}
using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Domain.Entities.User;
using FluentValidation.Results;
using MediatR;

namespace Epam.ItMarathon.ApiService.Application.UseCases.UserCases.Queries
{
    public record GetUsersQuery(string UserCode, ulong? UserId) : IRequest<Result<List<User>, ValidationResult>>;
}
using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Application.UseCases.UserCases.Queries;
using Epam.ItMarathon.ApiService.Domain.Abstract;
using Epam.ItMarathon.ApiService.Domain.Entities.User;
using Epam.ItMarathon.ApiService.Domain.Shared.ValidationErrors;
using FluentValidation.Results;
using MediatR;

namespace Epam.ItMarathon.ApiService.Application.UseCases.UserCases.Handlers
{
    public class GetUsersHandler(IUserReadOnlyRepository userRepository)
        : IRequestHandler<GetUsersQuery, Result<List<User>, ValidationResult>>
    {
        public async Task<Result<List<User>, ValidationResult>> Handle(GetUsersQuery request,
            CancellationToken cancellationToken)
        {
            var authUserResult =
                await userRepository.GetByCodeAsync(request.UserCode, cancellationToken, includeRoom: false,
                    includeWishes: true);
            if (authUserResult.IsFailure)
            {
                return authUserResult.ConvertFailure<List<User>>();
            }

            if (request.UserId is null)
            {
                // Get all users in room
                var roomId = authUserResult.Value.RoomId;
                var result = await userRepository.GetManyByRoomIdAsync(roomId, cancellationToken);
                return result;
            }

            // Otherwise, Get user by id
            var requestedUserResult =
                await userRepository.GetByIdAsync(request.UserId.Value, cancellationToken, includeRoom: false,
                    includeWishes: true);
            if (requestedUserResult.IsFailure)
            {
                return requestedUserResult.ConvertFailure<List<User>>();
            }

            if (requestedUserResult.Value.RoomId != authUserResult.Value.RoomId)
            {
                return Result.Failure<List<User>, ValidationResult>(
                    new NotAuthorizedError([
                        new ValidationFailure("id", "User with userCode and user with Id belongs to different rooms.")
                    ]));
            }

            return new List<User> { requestedUserResult.Value, authUserResult.Value };
        }
    }
}
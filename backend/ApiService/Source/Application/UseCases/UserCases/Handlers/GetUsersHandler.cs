using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Application.UseCases.UserCases.Queries;
using Epam.ItMarathon.ApiService.Domain.Abstract;
using Epam.ItMarathon.ApiService.Domain.Entities.User;
using Epam.ItMarathon.ApiService.Domain.Shared.ValidationErrors;
using FluentValidation.Results;
using MediatR;

namespace Epam.ItMarathon.ApiService.Application.UseCases.UserCases.Handlers
{
    public class GetUsersHandler(IRoomRepository roomRepository)
        : IRequestHandler<GetUsersQuery, Result<List<User>, ValidationResult>>
    {
        public async Task<Result<List<User>, ValidationResult>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var authUserResponse =
                await roomRepository.GetUserByUserCode(request.UserCode, includeRoom: false, includeWishes: true);
            if (authUserResponse.IsFailure)
            {
                return Result.Failure<List<User>, ValidationResult>(authUserResponse.Error);
            }

            if (request.UserId == null)
            {
                // Get all users in room
                var roomId = authUserResponse.Value.RoomId;
                var result = await roomRepository.GetRoomUsersByRoomId(roomId);
                return result;
            }

            // Otherwise, Get user by id
            var requestedUserResponse =
                await roomRepository.GetUserById(request.UserId.Value, includeRoom: false, includeWishes: true);
            if (requestedUserResponse.IsFailure)
            {
                return Result.Failure<List<User>, ValidationResult>(requestedUserResponse.Error);
            }
            if (requestedUserResponse.Value.RoomId != authUserResponse.Value.RoomId)
            {
                return Result.Failure<List<User>, ValidationResult>(
                    new NotAuthorizedError([
                        new ValidationFailure("id", "User with userCode and user with Id belongs to different rooms.")
                    ]));
            }

            return Result.Success<List<User>, ValidationResult>([requestedUserResponse.Value, authUserResponse.Value]);
        }
    }
}
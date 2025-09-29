using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Application.UseCases.UserCases.Commands;
using Epam.ItMarathon.ApiService.Domain.Abstract;
using Epam.ItMarathon.ApiService.Domain.Entities.User;
using Epam.ItMarathon.ApiService.Domain.Shared.ValidationErrors;
using FluentValidation.Results;
using MediatR;

namespace Epam.ItMarathon.ApiService.Application.UseCases.UserCases.Handlers
{
    public class CreateUserInRoomHandler(IRoomRepository roomRepository, IUserReadOnlyRepository userRepository) :
        IRequestHandler<CreateUserInRoomRequest, IResult<User, ValidationResult>>
    {
        public async Task<IResult<User, ValidationResult>> Handle(CreateUserInRoomRequest request, CancellationToken cancellationToken)
        {
            var roomCode = request.RoomCode;
            var user = request.User;
            var roomFindResult = await roomRepository.GetByRoomCodeAsync(roomCode);
            if (roomFindResult.IsFailure)
            {
                return roomFindResult.ConvertFailure<User>();
            }
            var userCode = Guid.NewGuid().ToString("N");
            var roomResult = roomFindResult.Value.AddUser(userBuilder =>
                userBuilder.WithAuthCode(userCode)
                .WithIsAdmin(false)
                .WithFirstName(user.FirstName)
                .WithLastName(user.LastName)
                .WithPhone(user.Phone)
                .WithEmail(user.Email)
                .WithDeliveryInfo(user.DeliveryInfo)
                .WithWantSurprise(user.WantSurprise)
                .WithInterests(user.Interests)
                .WithWishes(user.Wishes)
                );
            if (roomResult.IsFailure)
            {
               return roomResult.ConvertFailure<User>();
            }
            var result = await roomRepository.UpdateAsync(roomResult.Value);
            if (result.IsFailure)
            {
                return Result.Failure<User, ValidationResult>(new NotFoundError([
                    new ValidationFailure(nameof(roomCode), result.Error)
                ]));
            }
            return await userRepository.GetByCodeAsync(userCode);
        }
    }
}
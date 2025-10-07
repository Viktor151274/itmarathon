using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Application.UseCases.RoomCases.Commands;
using Epam.ItMarathon.ApiService.Domain.Abstract;
using Epam.ItMarathon.ApiService.Domain.Aggregate.Room;
using Epam.ItMarathon.ApiService.Domain.Shared.ValidationErrors;
using FluentValidation.Results;
using MediatR;

namespace Epam.ItMarathon.ApiService.Application.UseCases.RoomCases.Handlers
{
    public class UpdateRoomHandler(IRoomRepository roomRepository)
        : IRequestHandler<UpdateRoomCommand, Result<Room, ValidationResult>>
    {
        public async Task<Result<Room, ValidationResult>> Handle(UpdateRoomCommand request,
            CancellationToken cancellationToken)
        {
            var areAllFieldsEmpty = string.IsNullOrWhiteSpace(request.Name) &&
                                   string.IsNullOrWhiteSpace(request.Description) &&
                                   string.IsNullOrWhiteSpace(request.InvitationNote) &&
                                   !request.GiftExchangeDate.HasValue &&
                                   !request.GiftMaximumBudget.HasValue;

            if (areAllFieldsEmpty)
            {
                return Result.Failure<Room, ValidationResult>(
                    new BadRequestError([
                        new ValidationFailure(string.Empty, "At least one field must be provided.")
                    ]));
            }

            var roomResult = await roomRepository.GetByUserCodeAsync(request.UserCode);
            if (roomResult.IsFailure)
            {
                return roomResult;
            }

            var authUser = roomResult.Value.Users.First(user => user.AuthCode.Equals(request.UserCode));
            if (!authUser.IsAdmin)
            {
                return Result.Failure<Room, ValidationResult>(new ForbiddenError([
                    new ValidationFailure("userCode", "Only admin can update the room")
                ]));
            }

            var room = roomResult.Value;
            var validationResults = new[]
            {
                SetFieldIfNotNull(request.Name, room.SetName),
                SetFieldIfNotNull(request.Description, room.SetDescription),
                SetFieldIfNotNull(request.InvitationNote, room.SetInvitationNote),
                SetFieldIfNotNull(request.GiftExchangeDate, date => room.SetGiftExchangeDate(date!.Value)),
                SetFieldIfNotNull(request.GiftMaximumBudget, budget => room.SetGiftMaximumBudget(budget!.Value)),
            };

            var validationFailures = validationResults
                .Where(result => result.IsFailure)
                .SelectMany(result => result.Error.Errors)
                .ToList();

            if (validationFailures.Count > 0)
            {
                return Result.Failure<Room, ValidationResult>(new BadRequestError(validationFailures));
            }

            var updatingResult = await roomRepository.UpdateAsync(room);
            if (updatingResult.IsFailure)
            {
                return Result.Failure<Room, ValidationResult>(new BadRequestError([
                    new ValidationFailure(string.Empty, updatingResult.Error)
                ]));
            }

            var updatedRoomResult = await roomRepository.GetByUserCodeAsync(request.UserCode);
            return updatedRoomResult.Value;
        }

        private static Result<Room, ValidationResult> SetFieldIfNotNull<T>(T? fieldValue,
            Func<T, Result<Room, ValidationResult>> validationAction)
        {
            return fieldValue is not null ? validationAction(fieldValue) : Result.Success<Room?, ValidationResult>(null)!;
        }
    }
}
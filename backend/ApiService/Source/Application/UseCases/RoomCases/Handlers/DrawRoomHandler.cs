using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Application.UseCases.RoomCases.Commands;
using Epam.ItMarathon.ApiService.Domain.Abstract;
using Epam.ItMarathon.ApiService.Domain.Entities.User;
using Epam.ItMarathon.ApiService.Domain.Shared.ValidationErrors;
using FluentValidation.Results;
using MediatR;

namespace Epam.ItMarathon.ApiService.Application.UseCases.RoomCases.Handlers
{
    public class DrawRoomHandler(IRoomRepository roomRepository)
        : IRequestHandler<DrawRoomCommand, Result<List<User>, ValidationResult>>
    {
        public async Task<Result<List<User>, ValidationResult>> Handle(DrawRoomCommand request,
            CancellationToken cancellationToken)
        {
            // Get room by user.RoomId
            var roomResult = await roomRepository.GetByUserCodeAsync(request.UserCode, cancellationToken);
            if (roomResult.IsFailure)
            {
                return Result.Failure<List<User>, ValidationResult>(roomResult.Error);
            }

            // Get user by provided code and check user.IsAdmin
            var adminUser = roomResult.Value.Users.First(user => user.AuthCode.Equals(request.UserCode));
            if (!adminUser.IsAdmin)
            {
                return Result.Failure<List<User>, ValidationResult>(
                    new ForbiddenError([
                        new ValidationFailure("userCode", "Only admin can draw the room.")
                    ]));
            }

            // Draw room
            var drawResult = roomResult.Value.Draw();
            if (drawResult.IsFailure)
            {
                return Result.Failure<List<User>, ValidationResult>(drawResult.Error);
            }

            // Update room in DB
            var updatingResult = await roomRepository.UpdateAsync(drawResult.Value, cancellationToken);
            if (updatingResult.IsFailure)
            {
                return Result.Failure<List<User>, ValidationResult>(new BadRequestError([
                    new ValidationFailure(string.Empty, updatingResult.Error)
                ]));
            }

            return drawResult.Value.Users.ToList();
        }
    }
}
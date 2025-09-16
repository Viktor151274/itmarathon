using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Application.UseCases.RoomCases.Queries;
using Epam.ItMarathon.ApiService.Domain.Abstract;
using Epam.ItMarathon.ApiService.Domain.Aggregate.Room;
using FluentValidation.Results;
using MediatR;

namespace Epam.ItMarathon.ApiService.Application.UseCases.RoomCases.Handlers
{
    public class GetRoomHandler(IRoomRepository roomRepository)
        : IRequestHandler<GetRoomQuery, Result<Room, ValidationResult>>
    {
        public async Task<Result<Room, ValidationResult>> Handle(GetRoomQuery request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(request.UserCode))
            {
                return await roomRepository.GetByUserCodeAsync(request.UserCode!);
            }

            return await roomRepository.GetByRoomCodeAsync(request.RoomCode!);
        }
    }
}
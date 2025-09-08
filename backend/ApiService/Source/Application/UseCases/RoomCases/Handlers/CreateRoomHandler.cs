using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Application.UseCases.RoomCases.Commands;
using Epam.ItMarathon.ApiService.Domain.Abstract;
using Epam.ItMarathon.ApiService.Domain.Aggregate.Room;
using MediatR;

namespace Epam.ItMarathon.ApiService.Application.UseCases.RoomCases.Handlers
{
    public class CreateRoomHandler(IBaseRepository<Room> roomRepository) : IRequestHandler<CreateRoomCommand, Result<Room>>
    {
        public Task<Result<Room>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            var adminRequest = request.Admin;
            var roomRequest = request.Room;
            
            throw new NotImplementedException();
        }
    }
}

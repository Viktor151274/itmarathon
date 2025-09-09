using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Application.UseCases.RoomCases.Commands;
using Epam.ItMarathon.ApiService.Domain.Abstract;
using Epam.ItMarathon.ApiService.Domain.Aggregate.Room;
using Epam.ItMarathon.ApiService.Domain.Builders;
using MediatR;

namespace Epam.ItMarathon.ApiService.Application.UseCases.RoomCases.Handlers
{
    public class CreateRoomHandler(IBaseRepository<Room> roomRepository) : IRequestHandler<CreateRoomCommand, Result<Room>>
    {
        public async Task<Result<Room>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            var adminRequest = request.Admin;
            var roomRequest = request.Room;
            var room = RoomBuilder.Init()
                .WithName(roomRequest.Name)
                .WithDescription(roomRequest.Description)
                .WithGiftExchangeDate(roomRequest.GiftExchangeDate)
                .WithInvitationCode(roomRequest.InvitationNote)
                .WithGiftMaximumBudget(roomRequest.GiftMaximumBudget)
                .WithInvitationCode(Guid.NewGuid().ToString())
                .InitialAddUser(userBuilder =>
                userBuilder.WithAuthCode(Guid.NewGuid().ToString())
                .WithIsAdmin(true)
                .WithFirstName(adminRequest.FirstName)
                .WithLastName(adminRequest.LastName)
                .WithPhone(adminRequest.Phone)
                .WithEmail(adminRequest.Email)
                .WithDeliveryInfo(adminRequest.DeliveryInfo)
                .WithWantSurprise(adminRequest.WantSurprise)
                .WithInterests(adminRequest.Interests)
                .WithWishes(adminRequest.Wishes))
                .InitialBuild();
            if (room.IsFailure)
            { 
                return room; 
            }
            return await roomRepository.AddAsync(room.Value);
        }
    }
}

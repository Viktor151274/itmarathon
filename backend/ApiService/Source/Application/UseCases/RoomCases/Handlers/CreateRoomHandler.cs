using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Application.UseCases.RoomCases.Commands;
using Epam.ItMarathon.ApiService.Domain.Abstract;
using Epam.ItMarathon.ApiService.Domain.Aggregate.Room;
using Epam.ItMarathon.ApiService.Domain.Builders;
using FluentValidation.Results;
using MediatR;

namespace Epam.ItMarathon.ApiService.Application.UseCases.RoomCases.Handlers
{
    public class CreateRoomHandler(IRoomRepository roomRepository) : IRequestHandler<CreateRoomCommand, Result<Room, ValidationResult>>
    {
        public async Task<Result<Room, ValidationResult>> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            var adminRequest = request.Admin;
            var roomRequest = request.Room;
            var room = RoomBuilder.Init()
                .WithName(roomRequest.Name)
                .WithDescription(roomRequest.Description)
                .WithGiftExchangeDate(roomRequest.GiftExchangeDate)
                .WithGiftMaximumBudget(roomRequest.GiftMaximumBudget)
                .WithInvitationCode(Guid.NewGuid().ToString("N"))
                .InitialAddUser(userBuilder =>
                userBuilder.WithAuthCode(Guid.NewGuid().ToString("N"))
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

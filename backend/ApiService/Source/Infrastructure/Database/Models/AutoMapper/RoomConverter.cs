using AutoMapper;
using Epam.ItMarathon.ApiService.Domain.Builders;
using Epam.ItMarathon.ApiService.Infrastructure.Database.Models.Room;

namespace Epam.ItMarathon.ApiService.Infrastructure.Database.Models.AutoMapper
{
    internal class RoomConverter : ITypeConverter<RoomEf, Domain.Aggregate.Room.Room>
    {
        public Domain.Aggregate.Room.Room Convert(RoomEf source, Domain.Aggregate.Room.Room destination, ResolutionContext context)
        {
            var builder = RoomBuilder.Init()
                .WithId(source.Id)
                .WithCreatedOn(source.CreatedOn)
                .WithModifiedOn(source.ModifiedOn)
                .WithShouldBeClosedOn(source.ClosedOn)
                .WithInvitationCode(source.InvitationCode)
                .WithMinUsersLimit(source.MinUsersLimit)
                .WithMaxUsersLimit(source.MaxUsersLimit)
                .WithMaxWishesLimit(source.MaxWishesLimit)
                .WithName(source.Name)
                .WithDescription(source.Description)
                .WithInvitationNote(source.InvitationNote)
                .WithGiftExchangeDate(source.GiftExchangeDate)
                .WithGiftMaximumBudget(source.GiftMaximumBudget);
            foreach (var user in source.Users)
            {
                var wishesDict = user.Wishes.ToDictionary(
                    gift => gift.Name,
                    gift => gift.InfoLink
                    );
                builder.AddUser(
                    configure => {
                        var userBuilderConfiguration = configure.WithId(user.Id)
                        .WithCreatedOn(user.CreatedOn)
                        .WithModifiedOn(user.ModifiedOn)
                        .WithRoomId(user.RoomId)
                        .WithInterests(user.Interests)
                        .WithAuthCode(user.AuthCode)
                        .WithFirstName(user.FirstName)
                        .WithLastName(user.LastName)
                        .WithPhone(user.Phone)
                        .WithEmail(user.Email)
                        .WithDeliveryInfo(user.DeliveryInfo)
                        .WithGiftToUserId(user.GiftToUserId)
                        .WithWantSurprise(user.WantSurprise)
                        .WithInterests(user.Interests)
                        .WithIsAdmin(user.IsAdminForRoom != null)
                        .WithWishes(wishesDict);
                        if (user.TargetGift is not null)
                        userBuilderConfiguration.WithChosenGift(user.TargetGift.Name, user.TargetGift.InfoLink);
                        return userBuilderConfiguration;
                    });
            }
            var result = builder.Build();
            return result.Value;
        }
    }
}

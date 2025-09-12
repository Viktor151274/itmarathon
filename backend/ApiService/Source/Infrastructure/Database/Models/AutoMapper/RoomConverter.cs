using AutoMapper;
using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Domain.Builders;
using Epam.ItMarathon.ApiService.Infrastructure.Database.Models.Room;
using FluentValidation.Results;

namespace Epam.ItMarathon.ApiService.Infrastructure.Database.Models.AutoMapper
{
    internal class RoomConverter : ITypeConverter<RoomEf, Result<Domain.Aggregate.Room.Room, ValidationResult>>
    {
        public Result<Domain.Aggregate.Room.Room, ValidationResult> Convert(RoomEf source, Result<Domain.Aggregate.Room.Room, ValidationResult> destination, ResolutionContext context)
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
                        .WithWishes(wishesDict);
                        if (user.TargetGift is not null)
                        userBuilderConfiguration.WithChosenGift(user.TargetGift.Name, user.TargetGift.InfoLink);
                        if (user.Id == source.AdminId)
                            userBuilderConfiguration.WithIsAdmin(true);
                        else
                            userBuilderConfiguration.WithIsAdmin(false);
                        return userBuilderConfiguration;
                    });
            }
            var result = builder.Build();
            return result;
        }
    }
}

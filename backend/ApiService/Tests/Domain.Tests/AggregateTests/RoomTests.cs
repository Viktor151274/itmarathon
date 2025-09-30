using Epam.ItMarathon.ApiService.Domain.Builders;
using Epam.ItMarathon.ApiService.Domain.Shared.ValidationErrors;
using FluentAssertions;

namespace Epam.ItMarathon.ApiService.Domain.Tests.AggregateTests
{
    public class RoomTests
    {
        [Fact]
        public void Draw_ShouldReturnFailure_WhenNotEnoughUsers()
        {
            // Arrange
            var room = new RoomBuilder()
                .WithMinUsersLimit(2)
                .WithGiftExchangeDate(DateTime.UtcNow.AddDays(1))
                .AddUser(userBuilder => userBuilder.WithId(1).WithWishes(new ()))
                .Build();

            // Act
            var result = room.Value.Draw();

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<BadRequestError>();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals("room.MinUsersLimit"));
        }

        [Fact]
        public void Draw_ShouldReturnFailure_WhenRoomIsAlreadyClosed()
        {
            // Arrange
            var room = new RoomBuilder()
                .WithMinUsersLimit(0)
                .WithGiftExchangeDate(DateTime.UtcNow.AddDays(1))
                .WithShouldBeClosedOn(DateTime.UtcNow)
                .Build();

            // Act
            var result = room.Value.Draw();

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().BeOfType<BadRequestError>();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals("room.ClosedOn"));
        }

        [Theory]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(20)]
        [InlineData(100)]
        [InlineData(1000)]
        [InlineData(5000)]
        public void Draw_ShouldAssignGiftRecipients_WhenSuccessful(ulong usersToGenerate)
        {
            // Arrange
            var roomBuilder = new RoomBuilder()
                .WithMinUsersLimit(3)
                .WithMaxUsersLimit((uint)usersToGenerate)
                .WithGiftExchangeDate(DateTime.UtcNow.AddDays(1));

            for (ulong id = 1; id <= usersToGenerate; id++)
            {
                roomBuilder.AddUser(userBuilder =>
                    userBuilder.WithId(id).WithWishes(new ()));
            }

            var room = roomBuilder.Build();

            // Act
            var result = room.Value.Draw();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.ClosedOn.Should().NotBeNull();
            result.Value.ClosedOn.Should().BeOnOrBefore(DateTime.UtcNow);
            result.Value.Users.Should().OnlyHaveUniqueItems(u => u.GiftToUserId);
            result.Value.Users.Should()
                .NotContain(u => u.GiftToUserId == u.Id); // Ensure no user is assigned to themselves
        }
    }
}
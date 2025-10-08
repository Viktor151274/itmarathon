using Epam.ItMarathon.ApiService.Domain.Builders;
using FluentAssertions;

namespace Epam.ItMarathon.ApiService.Domain.Tests.BuildersTests
{
    public class RoomBuilderTests
    {
        [Fact]
        public void RoomBuilder_ShouldReturnFailure_WhenWantSurpriseFalseAndNoWishesProvided()
        {
            // Arrange & Act
            var result = new RoomBuilder()
                .AddUser(userBuilder => userBuilder
                    .WithFirstName("John")
                    .WithLastName("Doe")
                    .WithDeliveryInfo("Some info...")
                    .WithPhone("+380000000000")
                    .WithId(1)
                    .WithWantSurprise(false)
                    .WithWishes([]))
                .Build();

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals("Users[0].wishList"));
        }

        [Fact]
        public void RoomBuilder_ShouldReturnFailure_WhenWantSurpriseFalseAndInterestsAreProvided()
        {
            // Arrange & Act
            var result = new RoomBuilder()
                .AddUser(userBuilder => userBuilder
                    .WithFirstName("John")
                    .WithLastName("Doe")
                    .WithDeliveryInfo("Some info...")
                    .WithPhone("+380000000000")
                    .WithId(1)
                    .WithWantSurprise(false)
                    .WithInterests("Some interests...")
                    .WithWishes([("Test", null)]))
                .Build();

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals("Users[0].interests"));
        }

        [Fact]
        public void RoomBuilder_ShouldBuildSuccessfully_WhenWantSurpriseFalseAndValidWishesProvided()
        {
            // Arrange & Act
            var result = new RoomBuilder()
                .WithName("Test Room")
                .WithDescription("Test Room")
                .WithMinUsersLimit(1)
                .WithGiftExchangeDate(DateTime.UtcNow.AddDays(1))
                .AddUser(userBuilder => userBuilder
                    .WithFirstName("John")
                    .WithLastName("Doe")
                    .WithDeliveryInfo("Some info...")
                    .WithPhone("+380000000000")
                    .WithId(1)
                    .WithWantSurprise(false)
                    .WithWishes([("Test", null)]))
                .Build();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Users.First().WantSurprise.Should().BeFalse();
            result.Value.Users.First().Wishes.Should().Contain(wish => wish.Name!.Equals("Test"));
        }

        [Fact]
        public void RoomBuilder_ShouldReturnFailure_WhenWantSurpriseTrueAndNoInterestsProvided()
        {
            // Arrange & Act
            var result = new RoomBuilder()
                .AddUser(userBuilder => userBuilder
                    .WithFirstName("John")
                    .WithLastName("Doe")
                    .WithDeliveryInfo("Some info...")
                    .WithPhone("+380000000000")
                    .WithId(1)
                    .WithWantSurprise(true)
                    .WithWishes([]))
                .Build();

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Errors.Should().Contain(error =>
                error.PropertyName.Equals("Users[0].interests"));
        }

        [Fact]
        public void RoomBuilder_ShouldBuildSuccessfully_WhenWantSurpriseTrueAndValidInterestsProvided()
        {
            // Arrange & Act
            var result = new RoomBuilder()
                .WithName("Test Room")
                .WithDescription("Test Room")
                .WithMinUsersLimit(1)
                .WithGiftExchangeDate(DateTime.UtcNow.AddDays(1))
                .AddUser(userBuilder => userBuilder
                    .WithFirstName("John")
                    .WithLastName("Doe")
                    .WithDeliveryInfo("Some info...")
                    .WithPhone("+380000000000")
                    .WithId(1)
                    .WithWantSurprise(true)
                    .WithInterests("Some interests...")
                    .WithWishes([]))
                .Build();

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Users.First().WantSurprise.Should().BeTrue();
            result.Value.Users.First().Interests.Should().Be("Some interests...");
        }
    }
}
using Bogus;
using Epam.ItMarathon.ApiService.Application.Models.Creation;
using Epam.ItMarathon.ApiService.Domain.Aggregate.Room;
using Epam.ItMarathon.ApiService.Domain.Builders;

namespace Epam.ItMarathon.ApiService.Application.Tests
{
    /// <summary>
    /// Static class that contains fakers for generating test data.
    /// </summary>
    public static class DataFakers
    {
        /// <summary>
        /// Faker for generating UserApplication instances.
        /// </summary>
        public static Faker<UserApplication> UserApplicationFaker { get; private set; }

        /// <summary>
        /// Faker for generating Room instances.
        /// </summary>
        public static Faker<Room> RoomFaker { get; private set; }

        static DataFakers()
        {
            UserApplicationFaker = new Faker<UserApplication>()
                .RuleFor(user => user.FirstName, faker => faker.Name.FirstName())
                .RuleFor(user => user.LastName, faker => faker.Name.LastName())
                .RuleFor(user => user.Phone, faker => faker.Phone.PhoneNumber("+380#########"))
                .RuleFor(user => user.DeliveryInfo, faker => faker.Address.StreetAddress())
                .RuleFor(user => user.WantSurprise, _ => true)
                .RuleFor(user => user.Interests, faker => faker.Lorem.Word())
                .RuleFor(user => user.Wishes, _ => []);

            RoomFaker = new Faker<Room>().CustomInstantiator(faker => RoomBuilder.Init()
                .WithName(faker.Lorem.Word())
                .WithDescription(faker.Lorem.Word())
                .WithGiftExchangeDate(faker.Date.Future())
                .WithMinUsersLimit(10)
                .AddUser(userBuilder => userBuilder
                    .WithId((ulong)faker.IndexFaker + 1)
                    .WithFirstName(faker.Name.FirstName())
                    .WithLastName(faker.Name.LastName())
                    .WithPhone(faker.Phone.PhoneNumber("+380#########"))
                    .WithEmail(faker.Internet.Email())
                    .WithDeliveryInfo(faker.Address.StreetAddress())
                    .WithWantSurprise(true)
                    .WithInterests(faker.Lorem.Word())
                    .WithWishes([]))
                .Build().Value);
        }
    }
}
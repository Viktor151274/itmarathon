using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Domain.Abstract;
using Epam.ItMarathon.ApiService.Domain.Entities.User;
using Epam.ItMarathon.ApiService.Domain.Shared;

namespace Epam.ItMarathon.ApiService.Domain.Aggregate.Room
{
    public sealed class Room : BaseAggregate
    {
        internal const ulong MinUserLimitDefault = 3;
        internal const ulong MaxUserLimitDefault = 20;
        internal const ulong MaxWishesLimitDefault = 5;
        internal const int NameCharLimit = 40;
        internal const int DescriptionCharLimit = 200;
        internal const int InvitationNoteCharLimit = 1000;

        public DateTime? ClosedOn { get; private init; }
        public string InvitationCode { get; private set; }
        public ulong MinUsersLimit { get; private set; } = 3;
        public ulong MaxUsersLimit { get; private set; } = 20;
        public ulong MaxWishesLimit { get; private set; } = 3;
        public string Name { get; init; }
        public string Description { get; private set; }
        public string InvitationNote { get; private set; }
        public DateTime GiftExchangeDate { get; private set; }
        public ulong GiftMaximumBudget { get; private set; }
        public IList<User> Users { get; set; } = [];
        private Room() { }
        public static Result<Room> InitialCreate(string name, string description,
            string invitationNote, DateTime giftExchangeDate, ulong giftMaximumBudget,
            User admin, ulong minUsersLimit = MinUserLimitDefault,
            ulong maxUsersLimit = MaxUserLimitDefault, ulong MaxWishesLimit = MaxWishesLimitDefault)
        {
            var room = new Room()
            {
                InvitationCode = Guid.NewGuid().ToString(),
                MinUsersLimit = minUsersLimit,
                MaxUsersLimit = maxUsersLimit,
                MaxWishesLimit = MaxWishesLimit,
                Name = name,
                Description = description,
                InvitationNote = invitationNote,
                GiftExchangeDate = giftExchangeDate,
                GiftMaximumBudget = giftMaximumBudget,
            };
            room.Users.Add(admin);
            var roomValidator = new RoomValidator();
            var validationResult = roomValidator.Validate(room);
            if (!validationResult.IsValid)
            {
                return Result.Failure<Room>(validationResult.ToString(","));
            }
            return room;
        }
        public static Result<Room> Create(string name, string description,
            string invitationNote, DateTime giftExchangeDate, ulong giftMaximumBudget,
            IEnumerable<User> users, ulong minUsersLimit = MinUserLimitDefault,
            ulong maxUsersLimit = MaxUserLimitDefault, ulong MaxWishesLimit = MaxWishesLimitDefault)
        {
            var admin = users.Where(user => user.IsAdmin);
            if (admin.FirstOrDefault() is null || admin.Count() > 1) Result.Failure("The room should contain only one admin.");
            var room = new Room()
            {
                InvitationCode = Guid.NewGuid().ToString(),
                MinUsersLimit = minUsersLimit,
                MaxUsersLimit = maxUsersLimit,
                MaxWishesLimit = MaxWishesLimit,
                Name = name,
                Description = description,
                InvitationNote = invitationNote,
                GiftExchangeDate = giftExchangeDate,
                GiftMaximumBudget = giftMaximumBudget,
                Users = users.ToList()
            };
            var roomValidator = new RoomValidator();
            var validationResult = roomValidator.Validate(room);
            if (!validationResult.IsValid)
            {
                return Result.Failure<Room>(validationResult.ToString(","));
            }
            return room;
        }
    }
}

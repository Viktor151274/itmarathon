using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Domain.Abstract;
using Epam.ItMarathon.ApiService.Domain.Entities.User;
using FluentValidation.Results;

namespace Epam.ItMarathon.ApiService.Domain.Aggregate.Room
{
    public sealed class Room : BaseAggregate
    {
        internal const int NameCharLimit = 40;
        internal const int DescriptionCharLimit = 200;
        internal const int InvitationNoteCharLimit = 1000;
        public DateTime? ClosedOn { get; private init; }
        public string InvitationCode { get; private set; }
        public uint MinUsersLimit { get; private set; }
        public uint MaxUsersLimit { get; private set; }
        public uint MaxWishesLimit { get; private set; }
        public string Name { get; init; }
        public string Description { get; private set; }
        public string InvitationNote { get; private set; }
        public DateTime GiftExchangeDate { get; private set; }
        public ulong GiftMaximumBudget { get; private set; }
        public bool IsFull => Users.Count >= MaxUsersLimit;
        public IList<User> Users { get; set; } = [];
        private Room() { }
        public static Result<Room, ValidationResult> InitialCreate(DateTime? closedOn, string invitationCode, string name, string description,
            string invitationNote, DateTime giftExchangeDate, ulong giftMaximumBudget, IList<User> users,
            uint minUsersLimit, uint maxUsersLimit, uint maxWishesLimit)
        {
            var room = new Room()
            {
                ClosedOn = closedOn,
                InvitationCode = invitationCode,
                Name = name,
                Description = description,
                InvitationNote = invitationNote,
                GiftExchangeDate = giftExchangeDate,
                GiftMaximumBudget = giftMaximumBudget,
                Users = users,
                MinUsersLimit = minUsersLimit,
                MaxUsersLimit = maxUsersLimit,
                MaxWishesLimit = maxWishesLimit
            };
            var roomValidator = new RoomValidator();
            var validationResult = roomValidator.Validate(room);
            if (!validationResult.IsValid)
            {
                return Result.Failure<Room, ValidationResult>(validationResult);
            }
            return room;
        }
        public static Result<Room, ValidationResult> Create(ulong id, DateTime createdOn, DateTime modifiedOn,
            DateTime? closedOn, string invitationCode, string name, string description,
            string invitationNote, DateTime giftExchangeDate, ulong giftMaximumBudget, IList<User> users,
            uint minUsersLimit, uint maxUsersLimit, uint maxWishesLimit)
        {
            var admin = users.Where(user => user.IsAdmin);
            if (admin.FirstOrDefault() is null || admin.Count() > 1) Result.Failure("The room should contain only one admin.");
            var room = new Room()
            {
                Id = id,
                CreatedOn = createdOn,
                ModifiedOn = modifiedOn,
                ClosedOn = closedOn,
                InvitationCode = invitationCode,
                Name = name,
                Description = description,
                InvitationNote = invitationNote,
                GiftExchangeDate = giftExchangeDate,
                GiftMaximumBudget = giftMaximumBudget,
                Users = users,
                MinUsersLimit = minUsersLimit,
                MaxUsersLimit = maxUsersLimit,
                MaxWishesLimit = maxWishesLimit
            };
            var roomValidator = new RoomValidator();
            var validationResult = roomValidator.Validate(room);
            if (!validationResult.IsValid)
            {
                return Result.Failure<Room, ValidationResult>(validationResult);
            }
            return room;
        }
    }
}

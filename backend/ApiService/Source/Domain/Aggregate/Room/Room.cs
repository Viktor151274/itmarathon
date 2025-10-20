using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Domain.Abstract;
using Epam.ItMarathon.ApiService.Domain.Builders;
using Epam.ItMarathon.ApiService.Domain.Entities.User;
using Epam.ItMarathon.ApiService.Domain.Shared.ValidationErrors;
using Epam.ItMarathon.ApiService.Domain.Shared;
using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;

namespace Epam.ItMarathon.ApiService.Domain.Aggregate.Room
{
    public sealed class Room : BaseAggregate
    {
        internal const int NameCharLimit = 40;
        internal const int DescriptionCharLimit = 200;
        internal const int InvitationNoteCharLimit = 1000;
        internal const ulong RoomMaximumBudget = 100_000;

        public DateTime? ClosedOn { get; private set; }
        public string InvitationCode { get; private set; }
        public uint MinUsersLimit { get; private set; }
        public uint MaxUsersLimit { get; private set; }
        public uint MaxWishesLimit { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string InvitationNote { get; private set; }
        public DateTime GiftExchangeDate { get; private set; }
        public ulong GiftMaximumBudget { get; private set; }
        public bool IsFull => Users.Count >= MaxUsersLimit;
        public IList<User> Users { get; private set; } = [];
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

        public Result<Room, ValidationResult> SetName(string value)
        {
            return SetProperty(nameof(Name), room => room.Name = value);
        }

        public Result<Room, ValidationResult> SetDescription(string value)
        {
            return SetProperty(nameof(Description), room => room.Description = value);
        }

        public Result<Room, ValidationResult> SetInvitationNote(string value)
        {
            return SetProperty(nameof(InvitationNote), room => room.InvitationNote = value);
        }

        public Result<Room, ValidationResult> SetGiftExchangeDate(DateTime value)
        {
            return SetProperty(nameof(GiftExchangeDate), room => room.GiftExchangeDate = value.ToUniversalTime().Date);
        }

        public Result<Room, ValidationResult> SetGiftMaximumBudget(ulong value)
        {
            return SetProperty(nameof(GiftMaximumBudget), room => room.GiftMaximumBudget = value);
        }

        public Result<Room, ValidationResult> Draw()
        {
            // Room has MinUsersCount or more
            if (Users.Count < MinUsersLimit)
            {
                return Result.Failure<Room, ValidationResult>(
                    new BadRequestError([new ValidationFailure("room.MinUsersLimit", "Not enough users to draw the room.")
                    ]));
            }

            // Check room is not closed
            var roomCanBeModifiedResult = CheckRoomCanBeModified();
            if (roomCanBeModifiedResult.IsFailure)
            {
                return Result.Failure<Room, ValidationResult>(roomCanBeModifiedResult.Error);
            }

            var shuffledIds = Users.Select(user => user.Id).ToList();
            var random = new Random();

            // Shuffle list
            for (var idIndex = 0; idIndex < shuffledIds.Count-1; idIndex++)
            {
                var swapIndex = random.Next(idIndex+1, shuffledIds.Count);
                (shuffledIds[idIndex], shuffledIds[swapIndex]) = (shuffledIds[swapIndex], shuffledIds[idIndex]);
            }

            // Assign each participant their gift recipient
            for (var index = 0; index < Users.Count; index++)
            {
                Users[index].GiftRecipientUserId = shuffledIds[index];
            }

            ClosedOn = DateTime.UtcNow;
            return this;
        }

        public Result<Room, ValidationResult> AddUser(Func<UserBuilder, UserBuilder> userBuilderConfiguration)
        {
            if (ClosedOn is not null)
            {
                return Result.Failure<Room, ValidationResult>(
                    new BadRequestError([new ValidationFailure("room.ClosedOn", $"Room is already closed.")]));
            }
            var userBuilder = new UserBuilder();
            var user = userBuilderConfiguration(userBuilder).InitialBuild();
            Users.Add(user);
            var validationResult = new RoomValidator().Validate(this);
            if (!validationResult.IsValid)
            {
                Users.Remove(user);
                return Result.Failure<Room, ValidationResult>(validationResult);
            }
            return this;
        }

        private Result<bool, ValidationResult> CheckRoomCanBeModified()
        {
            if (ClosedOn is not null)
            {
                return Result.Failure<bool, ValidationResult>(
                    new BadRequestError([new ValidationFailure("room.ClosedOn", "Room is already closed.")
                ]));
            }

            return true;
        }

        private Result<Room, ValidationResult> SetProperty(string propertyName, Action<Room> setterExpression)
        {
            if (string.IsNullOrEmpty(propertyName))
            {
                return Result.Failure<Room, ValidationResult>(
                    new BadRequestError([
                        new ValidationFailure(nameof(propertyName), "Property name cannot be null or empty.")
                    ]));
            }

            // Check room is not closed
            var roomCanBeModifiedResult = CheckRoomCanBeModified();
            if (roomCanBeModifiedResult.IsFailure)
            {
                return Result.Failure<Room, ValidationResult>(roomCanBeModifiedResult.Error);
            }

            // Invoke expression to set value
            setterExpression(this);

            // Call a RoomValidator to validate updated property
            return ValidateProperty(char.ToLowerInvariant(propertyName[0]) + propertyName[1..]);
        }

        private Result<Room, ValidationResult> ValidateProperty(string propertyName)
        {
            var validationResult = new RoomValidator().Validate(this, 
                options => options.UseCustomSelector(new MemberNameValidatorSelector([propertyName])));
            return validationResult.IsValid ? this : Result.Failure<Room, ValidationResult>(validationResult);
        }
    }
}

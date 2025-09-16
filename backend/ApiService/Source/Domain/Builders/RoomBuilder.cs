using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Domain.Abstract;
using Epam.ItMarathon.ApiService.Domain.Aggregate.Room;
using Epam.ItMarathon.ApiService.Domain.Entities.User;
using FluentValidation.Results;

namespace Epam.ItMarathon.ApiService.Domain.Builders
{
    public class RoomBuilder : BaseAggregateBuilder<RoomBuilder>, IAggregateBuilder<Room>
    {
        private DateTime? _closedOn;
        private string _invitationCode;
        private uint _minUsersLimit = 3;
        private uint _maxUsersLimit = 20;
        private uint _maxWishesLimit  = 5;
        private string _name;
        private string _description;
        private string _invitationNote = "Hey!\nJoin our Secret Nick and make this holiday season magical! 🎄\nYou‘ll get to surprise someone with a gift — and receive one too. 🎅✨\nLet the holiday fun begin! 🌟 \n🎁 Join here:";
        private DateTime _giftExchangeDate;
        private ulong _giftMaximumBudget;
        private IList<User> _users { get; set; } = [];
        public static RoomBuilder Init() => new();
        public RoomBuilder WithShouldBeClosedOn(DateTime? closedOn)
        {
            _closedOn = closedOn;
            return this;
        }
        public RoomBuilder WithInvitationCode(string invitationCode)
        {
            _invitationCode = invitationCode;
            return this;
        }
        public RoomBuilder WithMinUsersLimit(uint minUsersLimit)
        {
            _minUsersLimit = minUsersLimit;
            return this;
        }
        public RoomBuilder WithMaxUsersLimit(uint maxUsersLimit)
        {
            _maxUsersLimit = maxUsersLimit;
            return this;
        }
        public RoomBuilder WithMaxWishesLimit(uint maxWishesLimit)
        {
            _maxWishesLimit = maxWishesLimit;
            return this;
        }
        public RoomBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public RoomBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public RoomBuilder WithInvitationNote(string giftExchangeDate)
        {
            _invitationNote = giftExchangeDate;
            return this;
        }
        public RoomBuilder WithGiftExchangeDate(DateTime giftExchangeDate)
        {
            _giftExchangeDate = giftExchangeDate;
            return this;
        }
        public RoomBuilder WithGiftMaximumBudget(ulong giftMaximumBudget)
        {
            _giftMaximumBudget = giftMaximumBudget;
            return this;
        }

        public RoomBuilder AddUser(Func<UserBuilder, UserBuilder> userBuilderConfiguration)
        {
            var userBuilder = new UserBuilder();
            var user = userBuilderConfiguration(userBuilder).Build();
            _users.Add(user);
            return this;
        }
        public RoomBuilder InitialAddUser(Func<UserBuilder, UserBuilder> userBuilderConfiguration)
        {
            var userBuilder = new UserBuilder();
            var user = userBuilderConfiguration(userBuilder).InitialBuild();
            _users.Add(user);
            return this;
        }
        public Result<Room, ValidationResult> Build()
        {
            return Room.Create(_id, _createdOn, _modifiedOn,
            _closedOn, _invitationCode, _name, _description,
            _invitationNote, _giftExchangeDate, _giftMaximumBudget, _users,
            _minUsersLimit, _maxUsersLimit, _maxWishesLimit);
        }

        public Result<Room, ValidationResult> InitialBuild()
        {
            return Room.InitialCreate(_closedOn, _invitationCode, _name, _description,
            _invitationNote, _giftExchangeDate, _giftMaximumBudget, _users,
            _minUsersLimit, _maxUsersLimit, _maxWishesLimit);
        }
    }
}

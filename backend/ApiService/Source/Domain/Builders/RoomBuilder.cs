using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Domain.Abstract;
using Epam.ItMarathon.ApiService.Domain.Aggregate.Room;
using Epam.ItMarathon.ApiService.Domain.Entities.User;

namespace Epam.ItMarathon.ApiService.Domain.Builders
{
    public class RoomBuilder : BaseAggregateBuilder<RoomBuilder>, IAggregateBuilder<Room>
    {
        private DateTime? _closedOn;
        private string _invitationCode;
        private ulong _minUsersLimit = 3;
        private ulong _maxUsersLimit = 20;
        private ulong _maxWishesLimit  = 3;
        private string _name;
        private string _description;
        private string _invitationNote;
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
        public RoomBuilder WithMinUsersLimit(ulong minUsersLimit)
        {
            _minUsersLimit = minUsersLimit;
            return this;
        }
        public RoomBuilder WithMaxUsersLimit(ulong maxUsersLimit)
        {
            _maxUsersLimit = maxUsersLimit;
            return this;
        }
        public RoomBuilder WithMaxWishesLimit(ulong maxWishesLimit)
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

        public RoomBuilder AddUser(Func<UserBuilder, UserBuilder> configure)
        {
            var userBuilder = new UserBuilder();
            var user = configure(userBuilder).Build();
            _users.Add(user);
            return this;
        }
        public RoomBuilder InitialAddUser(Func<UserBuilder, UserBuilder> configure)
        {
            var userBuilder = new UserBuilder();
            var user = configure(userBuilder).InitialBuild();
            _users.Add(user);
            return this;
        }
        public Result<Room> Build()
        {
            // TODO: Implement Builder w validation
            throw new NotImplementedException();
        }

        public Result<Room> InitialBuild()
        {
            // TODO: Implement initial Builder w validation
            throw new NotImplementedException();
        }
    }
}

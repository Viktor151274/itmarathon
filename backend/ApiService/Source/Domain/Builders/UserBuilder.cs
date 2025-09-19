using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Domain.Abstract;
using Epam.ItMarathon.ApiService.Domain.Entities.User;
using Epam.ItMarathon.ApiService.Domain.ValueObjects.Wish;
using FluentValidation.Results;

namespace Epam.ItMarathon.ApiService.Domain.Builders
{
    public class UserBuilder : BaseEntityBuilder<UserBuilder>
    {
        private ulong _roomId;
        private string _authCode;
        private string _firstName;
        private string _lastName;
        private string _phone;
        private string? _email;
        private string _deliveryInfo;
        private ulong? _giftToUserId;
        private Wish? _gift;
        private bool _wantSurprise;
        private string? _interests;
        private bool _isAdmin;
        private IEnumerable<Wish> _wishes;
        public UserBuilder WithRoomId(ulong roomId)
        {
            _roomId = roomId;
            return this;
        }
        public UserBuilder WithAuthCode(string authCode)
        {
            _authCode = authCode;
            return this;
        }
        public UserBuilder WithFirstName(string firstName)
        {
            _firstName = firstName;
            return this;
        }
        public UserBuilder WithLastName(string lastName)
        {
            _lastName = lastName;
            return this;
        }
        public UserBuilder WithPhone(string phone)
        {
            _phone = phone;
            return this;
        }
        public UserBuilder WithEmail(string? email)
        {
            _email = email;
            return this;
        }
        public UserBuilder WithDeliveryInfo(string deliveryInfo)
        {
            _deliveryInfo = deliveryInfo;
            return this;
        }
        public UserBuilder WithGiftToUserId(ulong? giftToUserId)
        {
            _giftToUserId = giftToUserId;
            return this;
        }
        public UserBuilder WithWantSurprise(bool wantSurprise)
        {
            _wantSurprise = wantSurprise;
            return this;
        }
        public UserBuilder WithInterests(string? interests)
        {
            _interests = interests;
            return this;
        }
        public UserBuilder WithIsAdmin(bool isAdmin)
        {
            _isAdmin = isAdmin;
            return this;
        }
        public UserBuilder WithChosenGift(string name, string? link)
        {
            _gift = Wish.Create(name, link);
            return this;
        }
        public UserBuilder WithWishes(Dictionary<string, string?> wishesDictionary)
        {
            _wishes = wishesDictionary.Select(pair => Wish.Create(pair.Key, pair.Value)).ToList();
            return this;
        }
        internal User Build()
        {
            return User.Create(_id, _createdOn, _modifiedOn,
                _roomId, _authCode, _firstName, _lastName, _phone, _email,
                _deliveryInfo, _giftToUserId, _gift, _wantSurprise, _interests, _isAdmin, _wishes);
        }
        internal User InitialBuild()
        {
            return User.InitialCreate(_roomId, _authCode, _firstName, _lastName, _phone, _email, _deliveryInfo,
                _wantSurprise, _interests, _isAdmin, _wishes
                );
        }
    }
}

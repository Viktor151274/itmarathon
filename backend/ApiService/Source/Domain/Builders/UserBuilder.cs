using Epam.ItMarathon.ApiService.Domain.Entities.User;
using Epam.ItMarathon.ApiService.Domain.ValueObjects.Wish;

namespace Epam.ItMarathon.ApiService.Domain.Factories
{
    public class UserBuilder
    {
        public ulong? _roomId;
        public string _authCode;
        public string _firstName;
        public string _lastName;
        public string _phone;
        public string? _email;
        public string _deliveryInfo;
        public ulong? _giftToUserId;
        public Wish? _gift;
        public bool _wantSurprise;
        public string? _interests;
        public bool _isAdmin;
        public IEnumerable<Wish> _wishes;

        internal User Build()
        {
            return User.Create();
        }
    }
}

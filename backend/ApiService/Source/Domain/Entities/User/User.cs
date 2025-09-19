using Epam.ItMarathon.ApiService.Domain.Abstract;
using Epam.ItMarathon.ApiService.Domain.ValueObjects.Wish;

namespace Epam.ItMarathon.ApiService.Domain.Entities.User
{
    public sealed class User : BaseEntity
    {
        internal static int FirstNameCharLimit = 40;
        internal static int LastNameCharLimit = 40;
        internal static int DeliveryInfoCharLimit = 500;
        internal static int InterestsCharLimit = 1000;
        public ulong RoomId { get; private set; }
        public string AuthCode { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Phone { get; private set; }
        public string? Email { get; private set; }
        public string DeliveryInfo { get; set; }
        public ulong? GiftToUserId { get; set; }
        public Wish? Gift { get; set; }
        public bool WantSurprise { get; set; }
        public string? Interests { get; set; }
        public bool IsAdmin { get; private set; }
        public IEnumerable<Wish> Wishes { get; set; }
        private User() { }
        internal static User InitialCreate(ulong roomId, string authCode,
            string firstName, string lastName, string phone, string? email,
            string deliveryInfo, bool wantSurprise, string? interests, bool isAdmin, IEnumerable<Wish> wishes)
        {
            var user = new User() {
                RoomId = roomId,
                AuthCode = authCode,
                FirstName = firstName,
                LastName = lastName,
                Phone = phone,
                Email = email,
                DeliveryInfo = deliveryInfo,
                WantSurprise = wantSurprise,
                Interests = interests,
                IsAdmin = isAdmin,
                Wishes = wishes
            };
            return user;
        }
        internal static User Create(ulong id, DateTime createdOn, DateTime modifiedOn,
            ulong roomId, string authCode,
            string firstName, string lastName, string phone, string? email,
            string deliveryInfo, ulong? giftToUserId, Wish? gift, bool wantSurprise, string? interests,
            bool isAdmin, IEnumerable<Wish> wishes)
        {
            var user = InitialCreate(roomId, authCode, firstName, lastName, phone, email, deliveryInfo,
                wantSurprise, interests, isAdmin, wishes);
            user.Id = id;
            user.ModifiedOn = modifiedOn;
            user.CreatedOn = createdOn;
            user.GiftToUserId = giftToUserId;
            user.Gift = gift;
            return user;
        }
        public void PromoteToAdmin()
        {
            IsAdmin = true;
        }
    }
}

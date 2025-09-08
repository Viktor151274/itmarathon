using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Domain.Abstract;
using Epam.ItMarathon.ApiService.Domain.Shared;
using Epam.ItMarathon.ApiService.Domain.ValueObjects.Wish;

namespace Epam.ItMarathon.ApiService.Domain.Entities.User
{
    public sealed class User : BaseEntity
    {
        internal static int FirstNameCharLimit = 40;
        internal static int LastNameCharLimit = 40;
        internal static int DeliveryInfoCharLimit = 500;
        internal static int InterestsCharLimit = 1000;
        public ulong? RoomId { get; private set; }
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
        internal static Result<User> InitialCreate(ulong? roomId, string firstName, string lastName, string phone, string? email,
            string deliveryInfo, bool wantSurprise, string? interests, IEnumerable<Wish> wishes)
        {
            var user = new User() {
                RoomId = roomId,
                FirstName = firstName,
                LastName = lastName,
                Phone = phone,
                Email = email,
                DeliveryInfo = deliveryInfo,
                WantSurprise = wantSurprise,
                Interests = interests,
                Wishes = wishes
            };
            var validator = new UserValidator();
            var validationResult = validator.Validate(user);
            if (!validationResult.IsValid)
            {
                return Result.Failure<User>(validationResult.ToString(","));
            }
            return user;
        }
        internal static Result<User> Create(ulong id, ulong? roomId, string firstName, string lastName, string phone, string? email,
            string deliveryInfo, bool wantSurprise, string? interests, IEnumerable<Wish> wishes)
        {
            var user = InitialCreate(roomId, firstName, lastName, phone, email, deliveryInfo, wantSurprise, interests, wishes);
            if (user.IsFailure)
            {
                return user;
            }
            user.Value.Id = id;
            user.Value.ModifiedOn = DateTime.UtcNow;
            return user;
        }
        public void PromoteToAdmin()
        {
            IsAdmin = true;
        }
    }
}

using CSharpFunctionalExtensions;
using Epam.ItMarathon.ApiService.Domain.ValueObjects.Wish;

namespace Epam.ItMarathon.ApiService.Domain.Entities.User
{
    public class User : BaseEntity
    {
        public static int FirstNameCharLimit = 40;
        public static int LastNameCharLimit = 40;
        public static int DeliveryInfoCharLimit = 500;
        public static int InterestsCharLimit = 1000;
        public ulong RoomId { get; private set; }
        public string AuthCode { get; private set; }
        public  string FirstName { get; private set; }
        public  string LastName { get; private set; }
        public  string Phone { get; private set; }
        public string? Email { get; private set; }
        public  string DeliveryInfo { get; set; }
        public ulong? GiftToUserId { get; set; }
        public ulong? GiftId { get; set; }
        public bool WantSurprise { get; set; }
        public string? Interests { get; set; }
        public bool IsAdmin { get; init; }
        public IEnumerable<Wish> Wishes { get; set; }
        private User() { }
        public Result<User> Create(ulong roomId, string firstName, string lastName, string phone, string? email,
            string deliveryInfo, bool wantSurprise, string? interests)
        {
            
        }
    }
}

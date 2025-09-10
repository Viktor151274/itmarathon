using Epam.ItMarathon.ApiService.Infrastructure.Database.Models.Gift;
using Epam.ItMarathon.ApiService.Infrastructure.Database.Models.Room;

namespace Epam.ItMarathon.ApiService.Infrastructure.Database.Models.User
{
    internal class UserEf : BaseModelEf
    {
        public ulong RoomId { get; set; }
        /// <summary>
        /// Authorization code of user.
        /// </summary>
        public required string AuthCode { get; set; }
        /// <summary>
        /// User's first name.
        /// </summary>
        public required string FirstName { get; set; }
        /// <summary>
        /// User's last name.
        /// </summary>
        public required string LastName { get; set; }
        /// <summary>
        /// Phone of user.
        /// </summary>
        public required string Phone { get; set; }
        /// <summary>
        /// User's email.
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// User's delivery information.
        /// </summary>
        public required string DeliveryInfo { get; set; }
        /// <summary>
        /// Id of user witch current user targets to gift.
        /// </summary>
        public ulong? GiftToUserId { get; set; }
        /// <summary>
        /// Id of gift witch user decided to gift.
        /// </summary>
        public ulong? GiftId { get; set; }
        /// <summary>
        /// Represents user's wish to have a surprise.
        /// </summary>
        public bool WantSurprise { get; set; }
        /// <summary>
        /// User's interests.
        /// </summary>
        public string? Interests { get; set; }
        /// <summary>
        /// Mapping property to a room witch user belongs to.
        /// </summary>
        public RoomEf Room { get; set; } = default!;
        /// <summary>
        /// Mapping property to a room where the user is admin, if he is.
        /// </summary>
        public RoomEf? IsAdminForRoom { get; set; }
        /// <summary>
        /// Number of gifts that user want to receive.
        /// </summary>
        public ICollection<GiftEf> Wishes { get; set; } = default!;
        /// <summary>
        /// Gift that user decided to gift to target user.
        /// </summary>
        public GiftEf? TargetGift { get; set; }
        /// <summary>
        /// User that current user chose to gift.
        /// </summary>
        public UserEf? TargetUser { get; set; }
        /// <summary>
        /// User from witch the current user will receive the gift.
        /// </summary>
        public UserEf? GotGiftFromUser { get; set; }
    }
}

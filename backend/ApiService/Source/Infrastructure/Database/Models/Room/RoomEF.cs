using Epam.ItMarathon.ApiService.Infrastructure.Database.Models.User;

namespace Epam.ItMarathon.ApiService.Infrastructure.Database.Models.Room
{
    internal class RoomEf : BaseModelEf
    {
        public DateTime? ClosedOn { get; set; }
        /// <summary>
        /// Unique identifier of room's owner.
        /// </summary>
        public ulong? AdminId { get; set; }
        /// <summary>
        /// Invitation code to the room.
        /// </summary>
        public required string InvitationCode { get; set; }
        /// <summary>
        /// Minimal amount of users in the room.
        /// </summary>
        public uint MinUsersLimit { get; set; } = 3;
        /// <summary>
        /// Maximal amount of users in the room.
        /// </summary>
        public uint MaxUsersLimit { get; set; } = 20;
        /// <summary>
        /// Minimal amount of wisher per user in the room.
        /// </summary>
        public uint MaxWishesLimit { get; set; } = 3;
        /// <summary>
        /// Name of room.
        /// </summary>
        public required string Name { get; init; }
        /// <summary>
        /// Description of the room.
        /// </summary>
        public required string Description { get; set; }
        /// <summary>
        /// Note included to invite.
        /// </summary>
        public required string InvitationNote { get; set; }
        /// <summary>
        /// Date of gifts to be exchanged.
        /// </summary>
        public DateTime GiftExchangeDate { get; set; }
        /// <summary>
        /// Maximum budged per gift.
        /// </summary>
        public ulong GiftMaximumBudget { get; set; }
        /// <summary>
        /// Mapping property to admin of the room.
        /// </summary>
        public UserEf Admin { get; set; } = default!;
        /// <summary>
        /// Mapping property to room's users.
        /// </summary>
        public ICollection<UserEf> Users { get; set; } = default!;
    }
}

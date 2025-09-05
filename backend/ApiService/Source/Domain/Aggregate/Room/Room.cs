using Epam.ItMarathon.ApiService.Domain.Entities;
using Epam.ItMarathon.ApiService.Domain.Entities.User;

namespace Epam.ItMarathon.ApiService.Domain.Aggregate.Room
{
    internal class Room : BaseEntity
    {
        public static ulong MinUserLimitDefault = 3;
        public static ulong MaxUserLimitDefault = 20;
        public static ulong MaxWishesLimitDefault = 5;
        public static int NameCharLimit = 40;
        public static int DescriptionCharLimit = 200;
        public static int InvitationNoteCharLimit = 1000;

        public DateTime? ClosedOn { get; private init; }
        public ulong AdminId { get; set; }
        public required string InvitationCode { get; set; }
        public uint MinUsersLimit { get; set; } = 3;
        public uint MaxUsersLimit { get; set; } = 20;
        public uint MaxWishesLimit { get; set; } = 3;
        public required string Name { get; init; }
        public required string Description { get; set; }
        public required string InvitationNote { get; set; }
        public DateTime GiftExchangeDate { get; set; }
        public ulong GiftMaximumBudget { get; set; }
        public IEnumerable<User> Users { get; set; }
        private Room() { }
        //public static Result<Room> Create()
        //{ 

        //}
    }
}

using Epam.ItMarathon.ApiService.Infrastructure.Database.Models.User;

namespace Epam.ItMarathon.ApiService.Infrastructure.Database.Models.Gift
{
    internal class GiftEf : BaseModelEf
    {
        /// <summary>
        /// Id of user witch gift belongs to.
        /// </summary>
        public ulong UserId { get; set; }
        /// <summary>
        /// Name of gift.
        /// </summary>
        public required string Name { get; set; }
        /// <summary>
        /// Link to gift.
        /// </summary>
        public string? InfoLink { get; set; }
        /// <summary>
        /// Mapping property to user.
        /// </summary>
        public UserEf? User { get; set; } = default!;
        /// <summary>
        /// Mapping property to user who chose to gift that gift.
        /// </summary>
        public UserEf? UserChose { get; set; } = default!;
    }
}

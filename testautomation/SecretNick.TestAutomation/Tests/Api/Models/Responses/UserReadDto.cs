using System.Text.Json.Serialization;
using Tests.Common.Models;
using Tests.Helpers;

namespace Tests.Api.Models.Responses
{
    public class UserReadDto
    {
        public long Id { get; set; }

        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime CreatedOn { get; set; }

        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime ModifiedOn { get; set; }

        public long RoomId { get; set; }
        public bool IsAdmin { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserCode { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public long? GiftToUserId { get; set; }
        public string? DeliveryInfo { get; set; }
        public bool? WantSurprise { get; set; }
        public string? Interests { get; set; }
        public List<WishDto>? WishList { get; set; }
    }
}

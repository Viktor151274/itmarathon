using System.Text.Json.Serialization;
using Tests.Helpers;

namespace Tests.Api.Models.Responses
{
    public class RoomReadDto
    {
        public required string Name { get; set; }
        public required string Description { get; set; }

        [JsonConverter(typeof(CustomDateTimeConverter))]
        public required DateTime GiftExchangeDate { get; set; }
        public required decimal GiftMaximumBudget { get; set; }
        public long Id { get; set; }

        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime CreatedOn { get; set; }

        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime ModifiedOn { get; set; }

        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime? ClosedOn { get; set; }
        public long AdminId { get; set; }
        public string? InvitationCode { get; set; }
        public string? InvitationNote { get; set; }
        public bool IsFull { get; set; }
    }
}

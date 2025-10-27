using System.Text.Json.Serialization;
using Tests.Helpers;

namespace Tests.Api.Models.Requests
{
    public class RoomPatchRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? InvitationNote { get; set; }

        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime? GiftExchangeDate { get; set; }

        public decimal? GiftMaximumBudget { get; set; }
    }
}

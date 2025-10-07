namespace Epam.ItMarathon.ApiService.Api.Dto.Requests.RoomRequests
{
    public class RoomPatchRequest
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? InvitationNote { get; set; }
        public DateTime? GiftExchangeDate { get; set; }
        public ulong? GiftMaximumBudget { get; set; }
    }
}
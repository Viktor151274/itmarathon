namespace Epam.ItMarathon.ApiService.Application.Models.Creation
{
    public class RoomApplication
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required DateTime GiftExchangeDate { get; set; }
        public required ulong GiftMaximumBudget { get; set; }
    }
}

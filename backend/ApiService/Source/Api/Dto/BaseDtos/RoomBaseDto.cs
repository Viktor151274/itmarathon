namespace Epam.ItMarathon.ApiService.Api.Dto.BaseDtos
{
    public class RoomBaseDto
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string GiftExchangeDate { get; set; }
        public required ulong GiftMaximumBudget { get; set; }
    }
}
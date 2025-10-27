namespace Tests.Common.Models
{
    public class RoomCreationDto
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public DateTime GiftExchangeDate { get; set; }
        public decimal GiftMaximumBudget { get; set; }
    }
}

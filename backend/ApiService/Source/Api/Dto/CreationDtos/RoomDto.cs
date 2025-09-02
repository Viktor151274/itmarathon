namespace Epam.ItMarathon.ApiService.Api.Dto.CreationDtos
{
    public class RoomDto
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string GiftExchangeDate { get; set; }
        public required ulong GiftMaximumBudget { get; set; }
        public IEnumerable<UserDto> Users { get; set; }
    }
}

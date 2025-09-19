namespace Epam.ItMarathon.ApiService.Api.Dto.CreationDtos
{
    public class UserCreationDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Phone { get; set; }
        public string? Email { get; set; }
        public required string DeliveryInfo { get; set; }
        public required bool WantSurprise { get; set; } = true;
        public string? Interests { get; set; }
        public IEnumerable<WishDto>? WishList { get; set; } 
    }
}

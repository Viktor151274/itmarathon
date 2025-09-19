namespace Epam.ItMarathon.ApiService.Api.Dto.CreationDtos
{
    public class UserReadDto
    {
        public ulong Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public ulong RoomId { get; set; }
        public bool IsAdmin { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? UserCode { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public ulong? GiftToUserId { get; set; }
        public string? DeliveryInfo { get; set; }
        public bool? WantSurprise { get; set; }
        public string? Interests { get; set; }
        public IEnumerable<WishDto>? WishList { get; set; }
    }
}
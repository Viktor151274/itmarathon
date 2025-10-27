using Tests.Api.Models.Requests;

namespace Tests.Common.Models
{
    public class UserCreationDto
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Email { get; set; } = "";
        public string DeliveryInfo { get; set; } = "";
        public bool WantSurprise { get; set; }
        public string? Interests { get; set; }
        public List<WishDto>? WishList { get; set; }
    }
}

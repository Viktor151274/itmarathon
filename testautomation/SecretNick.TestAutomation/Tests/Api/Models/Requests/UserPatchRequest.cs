using Tests.Common.Models;

namespace Tests.Api.Models.Requests
{
    public class UserPatchRequest
    {
        public bool? WantSurprise { get; set; }
        public string? Interests { get; set; }
        public List<WishDto>? WishList { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? DeliveryInfo { get; set; }
    }
}

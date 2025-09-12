namespace Epam.ItMarathon.ApiService.Application.Models.Creation
{
    public class UserApplication
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Phone { get; set; }
        public string? Email { get; set; }
        public required string DeliveryInfo { get; set; }
        public required bool WantSurprise { get; set; } = true;
        public string? Interests { get; set; }
        public Dictionary<string, string?> Wishes { get; set; }
    }
}

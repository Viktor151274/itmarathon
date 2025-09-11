namespace Epam.ItMarathon.ApiService.Api.Dto.CreationDtos
{
    public class RoomDto
    {
        public ulong Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime ClosedOn { get; set; }
        public string InvitationCode { get; set; }
        public string InvitationLink { get => $"https://frontendhost?userCode={InvitationCode}"; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string InvitationNote { get; set; }
        public DateTime GiftExchangeDate { get; set; }
        public ulong GiftMaximumBudget { get; set; }
    }
}

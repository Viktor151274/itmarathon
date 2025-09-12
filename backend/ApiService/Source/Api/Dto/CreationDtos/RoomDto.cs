namespace Epam.ItMarathon.ApiService.Api.Dto.CreationDtos
{
    public class RoomDto
    {
        public ulong Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime ClosedOn { get; set; }
        public string InvitationCode { get; set; }
        public string InvitationLink { get => $"https://frontendhost?roomCode={InvitationCode}"; }
        public string Name { get; set; }
        public string Description { get; set; }
        private string _invitationNote;
        public string InvitationNote { get => _invitationNote + InvitationLink; set => _invitationNote = value; }
        public DateTime GiftExchangeDate { get; set; }
        public ulong GiftMaximumBudget { get; set; }
    }
}

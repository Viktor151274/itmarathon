using Epam.ItMarathon.ApiService.Api.Extension;

namespace Epam.ItMarathon.ApiService.Api.Dto.CreationDtos
{
    public class RoomDto
    {
        public ulong Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime? ClosedOn { get; set; }
        public ulong AdminId { get; set; }
        public string InvitationCode { get; set; }
        public string InvitationLink
        {
            get
            {
                var builder = new UriBuilder(Variables.FrontendHostBaseUrl);
                var query = $"roomCode={Uri.EscapeDataString(InvitationCode)}";

                if (!string.IsNullOrEmpty(builder.Query))
                {
                    builder.Query = builder.Query.TrimStart('?') + "&" + query;
                }
                else
                {
                    builder.Query = query;
                }

                return builder.ToString();
            }
        }
        public string Name { get; set; }
        public string Description { get; set; }
        private string _invitationNote;
        public string InvitationNote { get => _invitationNote + InvitationLink; set => _invitationNote = value; }
        public DateTime GiftExchangeDate { get; set; }
        public ulong GiftMaximumBudget { get; set; }
    }
}

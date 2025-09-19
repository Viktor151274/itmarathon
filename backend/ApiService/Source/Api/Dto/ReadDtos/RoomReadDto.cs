using Epam.ItMarathon.ApiService.Api.Dto.BaseDtos;

namespace Epam.ItMarathon.ApiService.Api.Dto.ReadDtos
{
    public class RoomReadDto : RoomBaseDto
    {
        public ulong Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public DateTime? ClosedOn { get; set; }
        public ulong AdminId { get; set; }
        public string InvitationCode { get; set; }
        public string InvitationNote { get; set; }
        public bool IsFull { get; set; }
    }
}

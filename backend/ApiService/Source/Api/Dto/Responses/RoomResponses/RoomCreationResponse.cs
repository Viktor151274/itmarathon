using Epam.ItMarathon.ApiService.Domain.Aggregate.Room;

namespace Epam.ItMarathon.ApiService.Api.Dto.Responses.RoomResponses
{
    public class RoomCreationResponse
    {
        public Room Room { get; set; }
        public string UserCode { get; set; }
        public string UserLink { get; set; }
    }
}
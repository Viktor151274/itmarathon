using Epam.ItMarathon.ApiService.Api.Dto.CreationDtos;

namespace Epam.ItMarathon.ApiService.Api.Dto.Requests.RoomRequests
{
    public class RoomCreationRequest
    {
        public required RoomShortDto Room { get; set; }
        public required UserDto Admin { get; set; }
    }
}
using Epam.ItMarathon.ApiService.Api.Dto.CreationDtos;

namespace Epam.ItMarathon.ApiService.Api.Dto.Requests.RoomRequests
{
    public class RoomCreationRequest
    {
        public required RoomCreationDto Room { get; set; }
        public required UserCreationDto AdminUser { get; set; }
    }
}
using Epam.ItMarathon.ApiService.Api.Dto.ReadDtos;

namespace Epam.ItMarathon.ApiService.Api.Dto.Responses.RoomResponses
{
    public class RoomCreationResponse
    {
        public required RoomReadDto Room { get; set; }
        public required string UserCode { get; set; }
    }
}
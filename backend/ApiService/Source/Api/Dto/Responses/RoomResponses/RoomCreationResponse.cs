using Epam.ItMarathon.ApiService.Api.Dto.CreationDtos;
using Epam.ItMarathon.ApiService.Api.Dto.ReadDtos;

namespace Epam.ItMarathon.ApiService.Api.Dto.Responses.RoomResponses
{
    public class RoomCreationResponse
    {
        public required RoomReadDto RoomRead { get; set; }
        public required string UserCode { get; set; }
    }
}
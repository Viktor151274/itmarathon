using Epam.ItMarathon.ApiService.Api.Dto.CreationDtos;

namespace Epam.ItMarathon.ApiService.Api.Dto.Responses.RoomResponses
{
    public class RoomCreationResponse
    {
        public required RoomDto Room { get; set; }
        public required string UserCode { get; set; }
        public string UserLink { get => $"https://frontendhost?userCode={UserCode}"; }
    }
}
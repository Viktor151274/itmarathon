using Microsoft.AspNetCore.Mvc;

namespace Epam.ItMarathon.ApiService.Api.Dto.Requests.RoomRequests
{
    public record RoomReadingRequest([FromQuery(Name = "userCode")] string? UserCode, [FromQuery(Name = "roomCode")] string? RoomCode);
}
namespace Tests.Api.Models.Responses
{
    public class RoomCreationResponse
    {
        public required RoomReadDto Room { get; set; }
        public required string UserCode { get; set; }
    }
}

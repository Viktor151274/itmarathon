using Tests.Common.Models;

namespace Tests.Api.Models.Requests
{
    public class RoomCreationRequest
    {
        public required RoomCreationDto Room { get; set; }
        public required UserCreationDto AdminUser { get; set; }
    }
}

using Epam.ItMarathon.ApiService.Api.Dto.CreationDtos;
using Epam.ItMarathon.ApiService.Api.Extension;

namespace Epam.ItMarathon.ApiService.Api.Dto.Responses.RoomResponses
{
    public class RoomCreationResponse
    {
        public required RoomDto Room { get; set; }
        public required string UserCode { get; set; }
        public string UserLink
        {
            get
            {
                var builder = new UriBuilder(Variables.FrontendHostBaseUrl);
                var query = $"userCode={Uri.EscapeDataString(UserCode)}";

                if (!string.IsNullOrEmpty(builder.Query))
                {
                    builder.Query = builder.Query.TrimStart('?') + "&" + query;
                }
                else
                {
                    builder.Query = query;
                }

                return builder.ToString();
            }
        }
    }
}
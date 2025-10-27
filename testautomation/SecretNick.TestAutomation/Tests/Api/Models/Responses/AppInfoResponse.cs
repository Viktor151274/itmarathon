using System.Text.Json.Serialization;
using Tests.Helpers;

namespace Tests.Api.Models.Responses
{
    public class AppInfoResponse
    {
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime? DateTime { get; set; }

        public required string Environment { get; set; }

        public string? Build { get; set; }
    }
}

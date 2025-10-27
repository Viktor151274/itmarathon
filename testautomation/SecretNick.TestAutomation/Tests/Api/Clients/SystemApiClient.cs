using Microsoft.Playwright;
using Tests.Api.Models.Responses;

namespace Tests.Api.Clients
{
    public class SystemApiClient(IAPIRequestContext apiContext) : ApiClientBase(apiContext)
    {
        public async Task<AppInfoResponse> GetSystemInfoAsync()
        {
            var response = await ApiContext.GetAsync("/api/system/info");

            await ValidateResponseAsync(response, 200);
            return await DeserializeResponseAsync<AppInfoResponse>(response);
        }

        public async Task<(AppInfoResponse response, long elapsedMs)> GetSystemInfoWithTimingAsync()
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            var response = await GetSystemInfoAsync();
            stopwatch.Stop();

            return (response, stopwatch.ElapsedMilliseconds);
        }
    }
}

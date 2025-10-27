using System.Text.Json;
using Microsoft.Playwright;
using Serilog;
using Shouldly;

namespace Tests.Api.Clients
{
    public abstract class ApiClientBase(IAPIRequestContext apiContext)
    {
        protected readonly IAPIRequestContext ApiContext = apiContext;
        protected static readonly JsonSerializerOptions JsonOptions= new()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        protected static async Task<T> DeserializeResponseAsync<T>(IAPIResponse response) where T : class
        {
            var responseBody = await response.TextAsync();

            try
            {
                var result = JsonSerializer.Deserialize<T>(responseBody, JsonOptions);
                result.ShouldNotBeNull(
                    $"Failed to deserialize response to {typeof(T).Name}. Body: {responseBody}");
                return result;
            }
            catch (JsonException ex)
            {
                Log.Error(ex, "Deserialization failed. Response: {ResponseBody}", responseBody);
                throw new InvalidOperationException(
                    $"Failed to deserialize response to {typeof(T).Name}. Body: {responseBody}", ex);
            }
        }

        protected static async Task<(bool success, string? errorBody)> ValidateResponseAsync(
            IAPIResponse response, int expectedStatusCode, string requestInfo = "")
        {
            if (response.Status != expectedStatusCode)
            {
                var body = await response.TextAsync();

                Log.Error(
                    "API Request Failed\n" +
                    "Expected Status: {ExpectedStatus}\n" +
                    "Actual Status: {ActualStatus}\n" +
                    "Request: {RequestInfo}\n" +
                    "Response Body: {ResponseBody}",
                    expectedStatusCode, response.Status, requestInfo, body);

                throw new ApiException(expectedStatusCode, response.Status, body, requestInfo);
            }

            return (true, null);
        }

        protected static string SerializeRequest(object request)
        {
            try
            {
                return JsonSerializer.Serialize(request, JsonOptions);
            }
            catch
            {
                return request.ToString() ?? "null";
            }
        }
    }
}

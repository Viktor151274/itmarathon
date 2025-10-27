namespace Tests.Api.Clients
{
    public class ApiException(
        int expectedStatus,
        int actualStatus,
        string responseBody,
        string requestInfo = "") : Exception(FormatMessage(expectedStatus, actualStatus, responseBody, requestInfo))
    {
        public int ExpectedStatus { get; } = expectedStatus;
        public int ActualStatus { get; } = actualStatus;
        public string ResponseBody { get; } = responseBody;
        public string RequestInfo { get; } = requestInfo;

        private static string FormatMessage(
            int expectedStatus,
            int actualStatus,
            string responseBody,
            string requestInfo)
        {
            var message = $"Expected status {expectedStatus} but got {actualStatus}";

            if (!string.IsNullOrEmpty(requestInfo))
            {
                message += $"\nRequest: {requestInfo}";
            }

            if (!string.IsNullOrEmpty(responseBody))
            {
                message += $"\nResponse: {responseBody}";
            }

            return message;
        }
    }
}

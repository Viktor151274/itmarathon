using Reqnroll;
using Shouldly;
using Tests.Api.Clients;
using Tests.Api.Models.Responses;
using Tests.Helpers;

namespace Tests.Api.Steps
{
    [Binding]
    public class SystemApiSteps(ScenarioContext scenarioContext, SystemApiClient apiClient)
    {
        private readonly ScenarioContext _scenarioContext = scenarioContext;
        private readonly SystemApiClient _apiClient = apiClient;

        [When("I check the system health")]
        public async Task WhenICheckTheSystemHealth()
        {
            var response = await _apiClient.GetSystemInfoAsync();
            _scenarioContext.Set(response, "SystemInfo");
        }

        [When("I check the system health {int} times")]
        public async Task WhenICheckTheSystemHealthTimes(int times)
        {
            var results = new List<(long elapsedMs, bool success)>();

            for (int i = 0; i < times; i++)
            {
                try
                {
                    var (response, elapsedMs) = await _apiClient.GetSystemInfoWithTimingAsync();
                    results.Add((elapsedMs, true));
                }
                catch
                {
                    results.Add((0, false));
                }
            }

            _scenarioContext.Set(results, "PerformanceResults");
        }

        [Then("the system should be healthy")]
        public void ThenTheSystemShouldBeHealthy()
        {
            var systemInfo = _scenarioContext.Get<AppInfoResponse>("SystemInfo");
            systemInfo.ShouldNotBeNull();
            systemInfo.Environment.ShouldNotBeNullOrEmpty();
        }

        [Then("the response should include current date and time")]
        public void ThenTheResponseShouldIncludeCurrentDateAndTime()
        {
            var systemInfo = _scenarioContext.Get<AppInfoResponse>("SystemInfo");
            systemInfo.DateTime.ShouldNotBeNull();

            var responseTime = systemInfo.DateTime.Value.Simplify();
            var now = DateTime.Now.Simplify();

            responseTime.ShouldBeInRange(now.AddMinutes(-5), now.AddMinutes(5));
        }

        [Then("the response should include environment name")]
        public void ThenTheResponseShouldIncludeEnvironmentName()
        {
            var systemInfo = _scenarioContext.Get<AppInfoResponse>("SystemInfo");
            systemInfo.Environment.ShouldNotBeNullOrEmpty();
        }

        [Then("the response should include build version")]
        public void ThenTheResponseShouldIncludeBuildVersion()
        {
            var systemInfo = _scenarioContext.Get<AppInfoResponse>("SystemInfo");
            systemInfo.Build.ShouldNotBeNull();
        }

        [Then("all requests should succeed")]
        public void ThenAllRequestsShouldSucceed()
        {
            var results = _scenarioContext.Get<List<(long elapsedMs, bool success)>>("PerformanceResults");
            results.ShouldAllBe(r => r.success);
        }

        [Then("the average response time should be less than {int}ms")]
        public void ThenTheAverageResponseTimeShouldBeLessThanMs(int maxMs)
        {
            var results = _scenarioContext.Get<List<(long elapsedMs, bool success)>>("PerformanceResults");
            var successfulRequests = results.Where(r => r.success).ToList();

            successfulRequests.ShouldNotBeEmpty();

            var averageMs = successfulRequests.Average(r => r.elapsedMs);
            averageMs.ShouldBeLessThan(maxMs);
        }
    }
}

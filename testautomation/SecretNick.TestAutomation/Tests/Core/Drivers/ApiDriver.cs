using Microsoft.Playwright;
using Tests.Core.Configuration;

namespace Tests.Core.Drivers
{
    public interface IApiDriver : IAsyncDisposable
    {
        IAPIRequestContext Context { get; }
        Task InitializeAsync();
    }

    public class ApiDriver : IApiDriver
    {
        private IPlaywright? _playwright;
        private IAPIRequestContext? _apiContext;

        public IAPIRequestContext Context => _apiContext
            ?? throw new InvalidOperationException("API context not initialized. Call InitializeAsync first.");

        public async Task InitializeAsync()
        {
            _playwright = await Playwright.CreateAsync();

            _apiContext = await _playwright.APIRequest.NewContextAsync(new APIRequestNewContextOptions
            {
                BaseURL = ConfigManager.Settings.BaseUrls.Api,
                ExtraHTTPHeaders = new Dictionary<string, string>
                {
                    ["Accept"] = "application/json",
                    ["Content-Type"] = "application/json"
                },
                Timeout = ConfigManager.Settings.TestRun.DefaultTimeout
            });
        }

        public async ValueTask DisposeAsync()
        {
            if (_apiContext != null) await _apiContext.DisposeAsync();
            _playwright?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}

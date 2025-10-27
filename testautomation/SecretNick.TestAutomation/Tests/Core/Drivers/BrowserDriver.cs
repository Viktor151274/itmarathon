using Microsoft.Playwright;
using Tests.Core.Configuration;

namespace Tests.Core.Drivers
{
    public interface IBrowserDriver : IAsyncDisposable
    {
        IPage Page { get; }
        Task<IPage> CreateNewPageAsync();
        Task InitializeAsync();
    }

    public class BrowserDriver : IBrowserDriver
    {
        private IPlaywright? _playwright;
        private IBrowser? _browser;
        private IBrowserContext? _context;
        private readonly List<IPage> _pages = [];

        public IPage Page => _pages.FirstOrDefault()
            ?? throw new InvalidOperationException("No pages available. Call InitializeAsync first.");

        public async Task InitializeAsync()
        {
            _playwright = await Playwright.CreateAsync();

            var browserSettings = ConfigManager.Settings.Browser;

            _browser = browserSettings.Type.ToLowerInvariant() switch
            {
                "chromium" => await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = browserSettings.Headless,
                    SlowMo = browserSettings.SlowMo
                }),
                "firefox" => await _playwright.Firefox.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = browserSettings.Headless,
                    SlowMo = browserSettings.SlowMo
                }),
                "webkit" => await _playwright.Webkit.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = browserSettings.Headless,
                    SlowMo = browserSettings.SlowMo
                }),
                _ => throw new ArgumentException($"Unsupported browser type: {browserSettings.Type}")
            };

            _context = await _browser.NewContextAsync(new BrowserNewContextOptions
            {
                ViewportSize = new ViewportSize
                {
                    Width = browserSettings.ViewportWidth,
                    Height = browserSettings.ViewportHeight
                },
                Locale = "uk-UA",
                TimezoneId = "Europe/Kiev",
                IgnoreHTTPSErrors = true,
                AcceptDownloads = true
            });

            _context.SetDefaultTimeout(browserSettings.DefaultTimeout);

            var page = await _context.NewPageAsync();
            _pages.Add(page);
        }

        public async Task<IPage> CreateNewPageAsync()
        {
            if (_context == null)
                await InitializeAsync();

            var page = await _context!.NewPageAsync();
            _pages.Add(page);
            return page;
        }

        public async ValueTask DisposeAsync()
        {
            foreach (var page in _pages)
            {
                await page.CloseAsync();
            }
            _pages.Clear();

            if (_context != null) await _context.CloseAsync();
            if (_browser != null) await _browser.CloseAsync();
            _playwright?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}

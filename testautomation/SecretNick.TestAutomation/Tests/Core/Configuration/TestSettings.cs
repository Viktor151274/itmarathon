namespace Tests.Core.Configuration
{
    public class TestSettings
    {
        public BaseUrlsSettings BaseUrls { get; set; } = new();
        public BrowserSettings Browser { get; set; } = new();
        public TestRunSettings TestRun { get; set; } = new();
        public LoggingSettings Logging { get; set; } = new();
    }

    public class BaseUrlsSettings
    {
        public string Ui { get; set; } = string.Empty;
        public string Api { get; set; } = string.Empty;
    }

    public class BrowserSettings
    {
        public string Type { get; set; } = "chromium";
        public bool Headless { get; set; } = true;
        public int SlowMo { get; set; } = 0;
        public int DefaultTimeout { get; set; } = 30000;
        public int ViewportWidth { get; set; } = 1920;
        public int ViewportHeight { get; set; } = 1080;
    }

    public class TestRunSettings
    {
        public int DefaultTimeout { get; set; } = 30000;
    }

    public class LoggingSettings
    {
        public string Level { get; set; } = "Information";
        public string FilePath { get; set; } = "logs/test-run-{Date}.log";
    }
}

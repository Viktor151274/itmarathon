using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace Tests.Core.Configuration
{
    public class ConfigurationManager : IConfigurationManager
    {
        private readonly IConfiguration _configuration;
        private readonly Lazy<TestSettings> _settings;

        public TestSettings Settings => _settings.Value;

        public ConfigurationManager()
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                // 1. appsettings.json (lowest priority)
                .AddJsonFile("Core/Configuration/appsettings.json", optional: false, reloadOnChange: true);

            // 2. Environment variables (medium priority)
            // Use TEST_ prefix to avoid conflicts
            configBuilder.AddEnvironmentVariables(prefix: "TEST_");

            // 3. NUnit TestContext.Parameters (highest priority)
            // These come from --testparam or runsettings <TestRunParameters>
            configBuilder.AddInMemoryCollection(GetNUnitParameters());

            _configuration = configBuilder.Build();

            _settings = new Lazy<TestSettings>(() =>
            {
                var settings = new TestSettings();
                _configuration.Bind(settings);
                return settings;
            });
        }

        private Dictionary<string, string?> GetNUnitParameters()
        {
            var parameters = new Dictionary<string, string?>();

            try
            {
                // NUnit's TestContext.Parameters contains values from:
                // - dotnet test -- NUnit.TestParameter
                // - runsettings <TestRunParameters>
                if (TestContext.Parameters.Count > 0)
                {
                    foreach (var paramName in TestContext.Parameters.Names)
                    {
                        var value = TestContext.Parameters[paramName];
                        if (!string.IsNullOrEmpty(value))
                        {
                            parameters[paramName] = value;
                        }
                    }
                }
            }
            catch
            {
                // TestContext may not be available during initialization
                // This is fine - just means we're not in test execution context
            }

            return parameters;
        }

        public T GetValue<T>(string key)
        {
            var value = _configuration.GetValue<T>(key);
            return value ?? throw new InvalidOperationException($"Configuration key '{key}' not found");
        }

        public string GetValue(string key)
        {
            return _configuration[key] ?? throw new InvalidOperationException($"Configuration key '{key}' not found");
        }
    }
}

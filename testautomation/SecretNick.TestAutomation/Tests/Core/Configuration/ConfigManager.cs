

namespace Tests.Core.Configuration
{
    public static class ConfigManager
    {
        private static IConfigurationManager? _instance;
        private static readonly Lock _lock = new();

        public static void Initialize()
        {
            lock (_lock)
            {
                _instance = new ConfigurationManager();
            }
        }

        private static IConfigurationManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        _instance ??= new ConfigurationManager();
                    }
                }
                return _instance;
            }
        }

        public static TestSettings Settings => Instance.Settings;
        public static T GetValue<T>(string key) => Instance.GetValue<T>(key);
        public static string GetValue(string key) => Instance.GetValue(key);
    }
}


namespace Tests.Core.Configuration
{
    public interface IConfigurationManager
    {
        TestSettings Settings { get; }
        T GetValue<T>(string key);
        string GetValue(string key);
    }
}

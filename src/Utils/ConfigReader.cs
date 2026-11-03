using Newtonsoft.Json;

namespace DemoTest.Utils
{
    /// <summary>
    /// ConfigReader loads and provides access to test data
    /// stored in the TestData.json file.
    /// </summary>
    public static class ConfigReader
{
    private static dynamic configData;

    static ConfigReader()
    {
        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testData.json");
        var json = File.ReadAllText(path);
        configData = JsonConvert.DeserializeObject<dynamic>(json)
                     ?? throw new InvalidOperationException("Config file not loaded");
    }

        /// <summary>
        /// Get base URL from configuration.
        /// </summary>
        public static string GetBaseUrl()
        {
            return configData?.BaseUrl ?? string.Empty;
        }
    }
}
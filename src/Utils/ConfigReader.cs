using System.IO;
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
        var json = File.ReadAllText("src\\Data\\TestData.json");
        configData = JsonConvert.DeserializeObject<dynamic>(json)
                     ?? throw new InvalidOperationException("Config file not loaded");
    }

        /// <summary>
        /// Load configuration from TestData.json.
        /// This method should be called once at the beginning of the test run.
        /// </summary>
        public static void LoadConfig(string filePath = @"src\Data\TestData.json")
        {
            var json = File.ReadAllText(filePath);
            configData = JsonConvert.DeserializeObject<dynamic>(json)!; // force non-null
        }

        /// <summary>
        /// Get base URL from configuration.
        /// </summary>
        public static string GetBaseUrl()
        {
            return configData?.urls.baseUrl ?? string.Empty;
        }

        /// <summary>
        /// Get Careers page URL from configuration.
        /// </summary>
        public static string GetCareersUrl()
        {
            return configData?.urls.careersUrl ?? string.Empty;
        }

        /// <summary>
        /// Get explicit wait timeout value.
        /// </summary>
        public static int GetExplicitWait()
        {
            return (int)(configData?.timeouts.explicitWait ?? 5);
        }
         // Returns the home page title for validation in tests
        public static string GetHomeTitle()
        {
            return configData.expectedTexts.homeTitle;
        }
    }
}
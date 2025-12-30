using System;
using System.IO;
using Newtonsoft.Json;

namespace ChatBot.Helpers
{
    public class AppConfig
    {
        public string ApiKey { get; set; }
    }

    public static class ConfigHelper
    {
        private const string ConfigFileName = "config.json";

        public static AppConfig Load()
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string configPath = Path.Combine(basePath, ConfigFileName);

            if (!File.Exists(configPath))
            {
                throw new FileNotFoundException($"Configuration file not found: {configPath}. Please create one based on config.example.json.");
            }

            try
            {
                string json = File.ReadAllText(configPath);
                return JsonConvert.DeserializeObject<AppConfig>(json);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading configuration file: {ex.Message}", ex);
            }
        }
    }
}

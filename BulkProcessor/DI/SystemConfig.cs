using System.Configuration;

namespace BulkProcessor.DI
{
    public class SystemConfig : ISystemConfig
    {
        public string GetAppConfigKey(string key)
        {
            return ConfigurationSettings.AppSettings[key];
        }
    }
}

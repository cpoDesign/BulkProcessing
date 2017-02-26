namespace BulkProcessor.DI
{
    public interface ISystemConfig
    {
        string GetAppConfigKey(string key);
    }
}
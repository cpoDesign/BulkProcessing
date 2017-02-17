namespace BulkProcessor.Actors.SystemMessages
{
    internal class ConfigMessage
    {
        public string Key { get; private set; }

        public ConfigMessage(string key)
        {
            this.Key = key;
        }
    }
}
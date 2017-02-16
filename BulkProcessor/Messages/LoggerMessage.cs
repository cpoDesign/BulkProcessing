namespace BulkProcessor.Messages
{
    public class LoggerMessage
    {
        public MessageType MessageType { get; private set; }
        public string Message { get; private set; }

        public LoggerMessage(MessageType type, string message)
        {
            this.MessageType = type;
            this.Message = message;
        }
    }
}

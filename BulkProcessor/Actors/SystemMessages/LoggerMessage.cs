using BulkProcessor.Constants;

namespace BulkProcessor.Actors.SystemMessages
{
    public class LoggerMessage
    {
        public LoggerTypes MessageType { get; private set; }
        public string Message { get; private set; }

        public LoggerMessage(LoggerTypes type, string message)
        {
            this.MessageType = type;
            this.Message = message;
        }
    }
}

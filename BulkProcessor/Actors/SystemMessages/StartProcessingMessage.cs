using BulkProcessor.Constants;

namespace BulkProcessor.Actors.SystemMessages
{
    public class StartProcessingMessage
    {
        public MessageType MessageType { get; private set; }

        public StartProcessingMessage(MessageType messageType)
        {
            MessageType = messageType;
        }
    }
}

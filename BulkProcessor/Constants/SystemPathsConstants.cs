namespace BulkProcessor.Constants
{
    public class SystemPathsConstants
    {
        /// <summary>
        /// Path defined to be able to send messages to logger actor
        /// </summary>
        public static string LoggerActorPath = "/user/BulkProcessorActor/ProcessLoggerActor";

        /// <summary>
        /// Path define to be able to send messages to config actor
        /// </summary>
        public static string ConfigActorPath = "/user/BulkProcessorActor/ConfigActor";
    }
}

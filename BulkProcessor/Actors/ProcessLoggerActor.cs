using System;
using Akka.Actor;
using BulkProcessor.Messages;
using Common;

namespace BulkProcessor.Actors
{
    /// <summary>
    /// Monitoring actor, output logs into some place
    /// </summary>
    public class ProcessLoggerActor : ReceiveActor
    {
        public ProcessLoggerActor()
        {
            Receive<LoggerMessage>(message => LogMessage(message));
        }

        private void LogMessage(LoggerMessage message)
        {
            switch (message.MessageType)
            {
                case MessageType.Log:
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                }
                case MessageType.System:
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                }
                case MessageType.Trace:
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                }
            }

            Console.WriteLine(message.Message);
            Console.ForegroundColor = ConsoleColor.White;
        }
        #region lifecycle methods

        protected override void PreStart()
        {
            ConsoleLogger.LogMessage($"{this.GetType().Name} PreStart");

            base.PreStart();
        }

        protected override void PostStop()
        {
            ConsoleLogger.LogMessage($"{this.GetType().Name} PostStop");

            base.PostStop();
        }

        protected override void PreRestart(Exception reason, Object message)
        {
            ConsoleLogger.LogMessage($"{this.GetType().Name} PpreRestart because " + reason);
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ConsoleLogger.LogMessage($"{this.GetType().Name} PostRestart because " + reason);
            base.PostRestart(reason);
        }

        #endregion
    }
}
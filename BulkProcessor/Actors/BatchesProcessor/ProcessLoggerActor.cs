using System;
using Akka.Actor;
using Akka.Event;
using BulkProcessor.Actors.SystemMessages;
using BulkProcessor.Constants;

namespace BulkProcessor.Actors.BatchesProcessor
{
    /// <summary>
    /// Monitoring actor, output logs into some place
    /// </summary>
    public class ProcessLoggerActor : ReceiveActor
    {
        private ILoggingAdapter _logger = Context.GetLogger();

        public ProcessLoggerActor()
        {
            Receive<LoggerMessage>(message => LogMessage(message));
        }

        private void LogMessage(LoggerMessage message)
        {
            switch (message.MessageType)
            {
                case LoggerTypes.Log:
                    {
                        _logger.Info(message.Message);
                        break;
                    }
                case LoggerTypes.System:
                    {
                        _logger.Warning(message.Message);
                        break;
                    }
                case LoggerTypes.Trace:
                    {
                        _logger.Debug(message.Message);
                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }

        #region lifecycle methods

        protected override void PreStart()
        {
            _logger.Debug($"{this.GetType().Name} PreStart");

            base.PreStart();
        }

        protected override void PostStop()
        {
            _logger.Debug($"{this.GetType().Name} PostStop");

            base.PostStop();
        }

        protected override void PreRestart(Exception reason, Object message)
        {
            _logger.Debug($"{this.GetType().Name} PpreRestart because " + reason);
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            _logger.Debug($"{this.GetType().Name} PostRestart because " + reason);
            base.PostRestart(reason);
        }

        #endregion
    }
}
using System;
using Akka.Actor;
using BulkProcessor.Actors.SystemMessages;
using BulkProcessor.Constants;
using Common;

namespace BulkProcessor.Actors.BatchesProcessor.BulkProcessor
{
    /// <summary>
    /// Manages processing information about individual batch type
    /// </summary>
    public class BatchTypeManagerActor : ReceiveActor
    {
        private readonly MessageType _msgType;

        public BatchTypeManagerActor(MessageType msgType)
        {
            _msgType = msgType;

            Context.ActorSelection(SystemPathsConstants.LoggerActorPath).Tell(new LoggerMessage(LoggerTypes.Trace, $"Created new BatchTypeManagerActor: {msgType}"));

            Context.ActorOf(Props.Create<BatchTypeDataAccessActor>(), "BatchTypeDataAccessActor");
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
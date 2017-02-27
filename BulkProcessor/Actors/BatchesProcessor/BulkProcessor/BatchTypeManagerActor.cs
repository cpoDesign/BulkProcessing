using System;
using Akka.Actor;
using Akka.DI.Core;
using Akka.Event;
using BulkProcessor.Actors.BatchesProcessor.BulkProcessor.BatchTypeManager.Payments;
using BulkProcessor.Actors.BatchesProcessor.BulkProcessor.BatchTypeManager.Payments.Messages;
using BulkProcessor.Actors.SystemMessages;
using BulkProcessor.Constants;

namespace BulkProcessor.Actors.BatchesProcessor.BulkProcessor
{
    /// <summary>
    /// Manages processing information about individual batch type
    /// </summary>
    public class BatchTypeManagerActor : ReceiveActor
    {
        private ILoggingAdapter _logger = Context.GetLogger();
        private readonly MessageType _msgType;

        public BatchTypeManagerActor(MessageType msgType)
        {
            _msgType = msgType;

            Context.ActorSelection(SystemPathsConstants.LoggerActorPath).Tell(new LoggerMessage(LoggerTypes.Trace, $"Created new BatchTypeManagerActor: {msgType}"));

            switch (msgType)
            {
                case MessageType.Payments:
                    {
                        IActorRef jobCoordinator = Context.ActorOf(Context.DI().Props<JobCoordinatorActor>(), "JobCoordinator");

                        jobCoordinator.Tell(new ProcessFileMessage("Data\\PaymentsFile.csv"));
                        break;
                    }
            }

            Context.ActorOf(Props.Create<BatchTypeDataAccessActor>(), "BatchTypeDataAccessActor");
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
using System;
using Akka.Actor;
using Akka.Event;
using Akka.DI.Core;

namespace BulkProcessor.Actors.BatchesProcessor
{
    /// <summary>
    /// The overall process actor, manages all actors for processing system
    /// </summary>
    public class BulkProcessorActor : ReceiveActor
    {
        private ILoggingAdapter _logger = Context.GetLogger();

        public BulkProcessorActor()
        {
            _logger.Info("{0} actor created info", this.GetType().Name);

            Context.ActorOf(Props.Create<BatchesManagerActor>(), "BatchesManagerActor");
            Context.ActorOf(Props.Create<ProcessLoggerActor>(), "ProcessLoggerActor");

            // let DI to resove actor
            Context.ActorOf(Context.DI().Props<ConfigActor>(), "ConfigActor");
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

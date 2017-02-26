using System;
using Akka.Actor;
using Akka.Event;
using System.Threading.Tasks;
using Akka.Util;

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

            //_logger.Warning("{0} actor created warning", this.GetType().Name);

            //_logger.Debug("{0} actor created debug", this.GetType().Name);

            //_logger.Error("{0} actor created error", this.GetType().Name);

            Context.ActorOf(Props.Create<BatchesManagerActor>(), "BatchesManagerActor");
            Context.ActorOf(Props.Create<ProcessLoggerActor>(), "ProcessLoggerActor");
            Context.ActorOf(Props.Create<ConfigActor>(), "ConfigActor");
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


    /// <summary>
    /// Loads data about the batche
    /// </summary>
    public class BatchTypeDataAccessActor : ReceiveActor
    {

        private ILoggingAdapter _logger = Context.GetLogger();
        public BatchTypeDataAccessActor()
        {
            // var configActor = Context.ActorSelection(SystemPathsConstants.ConfigActorPath);

            //var t = Task.Run(async () =>
            //{
            //    var t1 = configActor.Ask(new ConfigMessage(SystemConstants.BatchSize), TimeSpan.FromSeconds(1));

            //    await Task.WhenAll(t1);

            //    return t1.Result;
            //});

            //t.PipeTo(configActor, Self);

            //configActor.Ask(new ConfigMessage(SystemConstants.BatchSize), TimeSpan.FromSeconds(1)).ContinueWith(t =>
            //{
            //    Console.WriteLine(t.Result);
            //});
            //Console.WriteLine(result.r);
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

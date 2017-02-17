using System;
using Akka.Actor;
using Common;
using System.Threading.Tasks;
using Akka.Util;

namespace BulkProcessor.Actors.BatchesProcessor
{
    /// <summary>
    /// The overall process actor, manages all actors for processing system
    /// </summary>
    public class BulkProcessorActor : ReceiveActor
    {
        public BulkProcessorActor()
        {
            ConsoleLogger.LogMessage($"{this.GetType().Name} actor created");

            Context.ActorOf(Props.Create<BatchesManagerActor>(), "BatchesManagerActor");
            Context.ActorOf(Props.Create<ProcessLoggerActor>(), "ProcessLoggerActor");
            Context.ActorOf(Props.Create<ConfigActor>(), "ConfigActor");
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


    /// <summary>
    /// Loads data about the batche
    /// </summary>
    public class BatchTypeDataAccessActor : ReceiveActor
    {
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

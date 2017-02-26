using System;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Event;
using BulkProcessor.Actors.SystemMessages;
using BulkProcessor.Constants;

namespace BulkProcessor.Actors.BatchesProcessor
{
    /// <summary>
    /// Loads data about the batche
    /// </summary>
    public class BatchTypeDataAccessActor : ReceiveActor
    {
        private ILoggingAdapter _logger = Context.GetLogger();
        public BatchTypeDataAccessActor()
        {
            var configActor = Context.ActorSelection(SystemPathsConstants.ConfigActorPath);
            var message = new ConfigMessage(SystemConstants.BatchSize);
            configActor.Tell(message, Self);

            //Task.Run(async () =>
            //{
            //    var t1 = configActor.Ask(new ConfigMessage(SystemConstants.BatchSize), TimeSpan.FromSeconds(1));

            //    await Task.WhenAll(t1);

            //    return t1.Result;
            //}).PipeTo(configActor, Self);

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
using System;
using Akka.Actor;
using BulkProcessor.Messages;
using Common;

namespace BulkProcessor.Actors
{
    /// <summary>
    /// the overall process
    /// </summary>
    public class BulkProcessorActor : ReceiveActor
    {
        public BulkProcessorActor()
        {
            ConsoleLogger.LogMessage($"{this.GetType() .Name} actor created");

            Context.ActorOf(Props.Create<BatchesManagerActor>(), "BatchesManagerActor");
            Context.ActorOf(Props.Create<ProcessLoggerActor>(), "ProcessLoggerActor");
        }

        #region lifecycle methods

        protected override void PreStart()
        {
            ConsoleLogger.LogMessage($"{this.GetType() .Name} PreStart");

            base.PreStart();
        }

        protected override void PostStop()
        {
            ConsoleLogger.LogMessage($"{this.GetType() .Name} PostStop");

            base.PostStop();
        }

        protected override void PreRestart(Exception reason, Object message)
        {
            ConsoleLogger.LogMessage($"{this.GetType() .Name} PpreRestart because " + reason);
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ConsoleLogger.LogMessage($"{this.GetType() .Name} PostRestart because " + reason);
            base.PostRestart(reason);
        }

        #endregion
    }

    /// <summary>
    /// contains information about the batch types and orchestrate them
    /// </summary>
    public class BatchesManagerActor : ReceiveActor
    {
        public BatchesManagerActor()
        {
            Receive<StartBulkProcessingMessage>(message => StartProcessingMessages(message));
        }

        private void StartProcessingMessages(StartBulkProcessingMessage message)
        {
            Context.ActorSelection(Constants.LoggerPath).Tell(new LoggerMessage(MessageType.Log, $"{DateTime.Now.ToString("T")}: Starting processing message recieved"));
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
    /// Manages processing information about individual batch type
    /// </summary>
    public class BatchTypeManagerActor : ReceiveActor
    {
        public BatchTypeManagerActor()
        {
            Context.ActorOf(Props.Create<BatchTypeDataAccessActor>(), "BatchTypeDataAccessActor");

            Context.ActorOf(Props.Create<BatchTypeManagerActor>(), "BatchTypeManagerActorType-1");
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

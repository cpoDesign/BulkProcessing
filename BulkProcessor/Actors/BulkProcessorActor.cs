using System;
using System.Collections.Generic;
using Akka.Actor;
using BulkProcessor.Messages;
using Common;

namespace BulkProcessor.Actors
{
    /// <summary>
    /// the overall process, manages all actors for all processors
    /// </summary>
    public class BulkProcessorActor : ReceiveActor
    {
        public BulkProcessorActor()
        {
            ConsoleLogger.LogMessage($"{this.GetType().Name} actor created");

            Context.ActorOf(Props.Create<BatchesManagerActor>(), "BatchesManagerActor");
            Context.ActorOf(Props.Create<ProcessLoggerActor>(), "ProcessLoggerActor");
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
    /// contains information about the batch types and orchestrate them
    /// manages all different types on messages and created related actors
    /// </summary>
    public class BatchesManagerActor : ReceiveActor
    {
        private Dictionary<MessageType, IActorRef> _registeredMessageTypes;

        public BatchesManagerActor()
        {
            _registeredMessageTypes = new Dictionary<MessageType, IActorRef>();

            // trigerred processing
            Receive<StartBulkProcessingMessage>(message => StartProcessingMessages(message));
        }

        private void StartProcessingMessages(StartBulkProcessingMessage message)
        {
            Context.ActorSelection(Constants.LoggerPath).Tell(new LoggerMessage(MessageType.Log, $"{DateTime.Now.ToString("T")}: Starting processing message recieved"));

            foreach (string messageTypeName in Enum.GetNames(typeof(MessageType)))
            {
                MessageType msgType;
                Enum.TryParse(messageTypeName, out msgType);

                IActorRef newChildActor = Context.ActorOf(Props.Create(() => new BatchTypeManagerActor(msgType)), $"BatchTypeManagerActor{messageTypeName}");

                _registeredMessageTypes.Add(msgType, newChildActor);

            }
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
        private readonly MessageType _msgType;

        public BatchTypeManagerActor(MessageType msgType)
        {
            _msgType = msgType;

            Context.ActorSelection(Constants.LoggerPath).Tell(new LoggerMessage(MessageType.Trace, $"Created new BatchTypeManagerActor: {msgType}"));

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
}

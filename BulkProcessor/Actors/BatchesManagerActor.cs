using System;
using System.Collections.Generic;
using Akka.Actor;
using Akka.Event;
using BulkProcessor.Actors.BatchesProcessor.BulkProcessor;
using BulkProcessor.Actors.SystemMessages;
using BulkProcessor.Constants;

namespace BulkProcessor.Actors
{
    /// <summary>
    /// contains information about the batch types and orchestrate them
    /// manages all different types on messages and created related actors
    /// </summary>
    public class BatchesManagerActor : ReceiveActor
    {
        private readonly Dictionary<MessageType, IActorRef> _registeredMessageTypes;

        private ILoggingAdapter _logger = Context.GetLogger();

        public BatchesManagerActor()
        {
            _registeredMessageTypes = new Dictionary<MessageType, IActorRef>();

            // trigerred processing
            Receive<StartBulkProcessingMessage>(message => StartProcessingMessages(message));
        }

        /// <summary>
        /// When we recieve the message we will start processing, by creating 
        /// </summary>
        /// <param name="message"></param>
        void StartProcessingMessages(StartBulkProcessingMessage message) 
        {
            Context.ActorSelection(SystemPathsConstants.LoggerActorPath).Tell(new LoggerMessage(LoggerTypes.Trace, $"Starting processing message recieved"));

            // For each message type we have we can generate a processor and hold the reference in collection. 
            // This is because we can after find out which has been registered.
            foreach (string messageTypeName in Enum.GetNames(typeof(MessageType)))
            {
                MessageType msgType;
                Enum.TryParse(messageTypeName, out msgType);

                IActorRef newChildActor;
                if (!_registeredMessageTypes.ContainsKey(msgType))
                {
                    // here we create new child actor
                    newChildActor = Context.ActorOf(Props.Create(() => new BatchTypeManagerActor(msgType)),
                        $"BatchTypeManagerActor{messageTypeName}");
                    _registeredMessageTypes.Add(msgType, newChildActor);
                }
                else
                {
                    newChildActor = _registeredMessageTypes[msgType];
                }

                // now lets tell the actor to start processing the message
                newChildActor.Tell(new StartProcessingMessage(msgType));
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
using System;
using System.Collections.Generic;
using System.Configuration;
using Akka.Actor;
using Akka.Event;
using BulkProcessor.Actors.SystemMessages;

namespace BulkProcessor.Actors.BatchesProcessor
{
    /// <summary>
    /// Monitoring actor, output logs into some place
    /// </summary>
    public class ConfigActor : ReceiveActor
    {
        private Dictionary<string, string> _configurationDictionary;

        private ILoggingAdapter _logger = Context.GetLogger();
        public ConfigActor()
        {
            Receive<ConfigMessage>(message =>
            {
                var value = GetConfig(message); 
                _logger.Debug("Recieved request for {0} confing with value {1}", message.Key, value);
                Sender.Tell(value, Self);
            });
        }

        string GetConfig(ConfigMessage message)
        {
            if (!_configurationDictionary.ContainsKey(message.Key))
            {
                var value = ConfigurationSettings.AppSettings[message.Key];
                if (string.IsNullOrWhiteSpace(value))
                {
                    var e =  new ConfigurationException($"Failed to find config with name:{message.Key}");

                    Sender.Tell(new Failure { Exception = e }, Self);
                }

                _configurationDictionary.Add(message.Key, value);
            }

            return _configurationDictionary[message.Key];
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
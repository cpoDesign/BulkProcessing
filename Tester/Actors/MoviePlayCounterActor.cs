using Akka.Actor;
using System;
using System.Collections.Generic;
using Tester.Messages;

namespace Tester.Actors
{
    public class MoviePlayCounterActor : ReceiveActor
    {
        private readonly Dictionary<string, int> _moviePlayCounts;

        public MoviePlayCounterActor()
        {
            _moviePlayCounts = new Dictionary<string, int>();
            Receive<IncrementPlayCountMessage>(message => HandleIncrementCount(message));
        }

        private void HandleIncrementCount(IncrementPlayCountMessage message)
        {
            if (!_moviePlayCounts.ContainsKey(message.MovieTitle))
            {
                _moviePlayCounts.Add(message.MovieTitle, 1);
            }
            else
            {
                _moviePlayCounts[message.MovieTitle]++;
            }

            ConsoleLogger.LogMessage($"Movie: {message.MovieTitle} has been played {_moviePlayCounts[message.MovieTitle]}");
        }

        #region Lifetime hooks
        protected override void PreStart()
        {
            ConsoleLogger.LogTraceMessage("MoviePlayCounterActor PreStart");

            base.PreStart();
        }
        protected override void PostStop()
        {
            ConsoleLogger.LogTraceMessage("MoviePlayCounterActor PostStop");

            base.PostStop();
        }

        protected override void PreRestart(Exception reason, Object message)
        {
            ConsoleLogger.LogTraceMessage("MoviePlayCounterActor PpreRestart because " + reason);
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ConsoleLogger.LogTraceMessage("MoviePlayCounterActor PostRestart because " + reason);
            base.PostRestart(reason);
        }
        #endregion
    }
}
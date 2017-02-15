using Akka.Actor;
using System;

namespace Tester.Actors
{
    public class MoviePlayCounterActor : ReceiveActor
    {
        public MoviePlayCounterActor()
        {

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
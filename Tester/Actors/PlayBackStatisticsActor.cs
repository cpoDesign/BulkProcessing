using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace Tester.Actors
{
    public class PlayBackStatisticsActor : ReceiveActor
    {
        public PlayBackStatisticsActor()
        {
            Context.ActorOf(Props.Create<MoviePlayCounterActor>(), "MoviePlayCounter");
        }

        #region Lifetime hooks
        protected override void PreStart()
        {
            ConsoleLogger.LogTraceMessage("PlayBackStatisticsActor PreStart");

            base.PreStart();
        }
        protected override void PostStop()
        {
            ConsoleLogger.LogTraceMessage("PlayBackStatisticsActor PostStop");

            base.PostStop();
        }

        protected override void PreRestart(Exception reason, Object message)
        {
            ConsoleLogger.LogTraceMessage("PlayBackStatisticsActor PpreRestart because " + reason);
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ConsoleLogger.LogTraceMessage("PlayBackStatisticsActor PostRestart because " + reason);
            base.PostRestart(reason);
        } 
        #endregion
    }
}

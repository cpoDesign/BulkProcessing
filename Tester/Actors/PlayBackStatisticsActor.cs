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

        }

        protected override void PreStart()
        {
            ConsoleLogger.LogMessage("PlayBackStatisticsActor PreStart");

            base.PreStart();
        }
        protected override void PostStop()
        {
            ConsoleLogger.LogMessage("PlayBackStatisticsActor PostStop");

            base.PostStop();
        }

        protected override void PreRestart(Exception reason, Object message)
        {
            ConsoleLogger.LogMessage("PlayBackStatisticsActor PpreRestart because " + reason);
            base.PreRestart(reason, message);
        }


        protected override void PostRestart(Exception reason)
        {
            ConsoleLogger.LogMessage("PlayBackStatisticsActor PostRestart because " + reason);
            base.PostRestart(reason);
        }
    }
}

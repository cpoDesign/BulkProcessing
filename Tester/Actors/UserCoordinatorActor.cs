using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace Tester.Actors
{
    public class UserCoordinatorActor: ReceiveActor
    {
        public UserCoordinatorActor()
        {

        }

        protected override void PreStart()
        {
            ConsoleLogger.LogMessage("UserCoordinatorActor PreStart");

            base.PreStart();
        }
        protected override void PostStop()
        {
            ConsoleLogger.LogMessage("UserCoordinatorActor PostStop");

            base.PostStop();
        }

        protected override void PreRestart(Exception reason, Object message)
        {
            ConsoleLogger.LogMessage("UserCoordinatorActor PpreRestart because " + reason);
            base.PreRestart(reason, message);
        }


        protected override void PostRestart(Exception reason)
        {
            ConsoleLogger.LogMessage("UserCoordinatorActor PostRestart because " + reason);
            base.PostRestart(reason);
        }
    }
}

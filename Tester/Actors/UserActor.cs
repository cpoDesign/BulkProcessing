using System;
using Akka.Actor;

namespace Tester.Actors
{
    public class UserActor : ReceiveActor
    {
        int UserId { get; set; }
        public UserActor(int userId)
        {
            this.UserId = userId;
            ConsoleLogger.LogMessage("User actor created");
        }

        protected override void PreStart()
        {
            ConsoleLogger.LogMessage("UserActor PreStart");

            base.PreStart();
        }
        protected override void PostStop()
        {
            ConsoleLogger.LogMessage("UserActor PostStop");

            base.PostStop();
        }

        protected override void PreRestart(Exception reason, Object message)
        {
            ConsoleLogger.LogMessage("UserActor PpreRestart because " + reason);
            base.PreRestart(reason, message);
        }


        protected override void PostRestart(Exception reason)
        {
            ConsoleLogger.LogMessage("UserActor PostRestart because " + reason);
            base.PostRestart(reason);
        }
    }
}

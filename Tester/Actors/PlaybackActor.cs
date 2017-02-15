using Akka.Actor;
using Common;
using System;

namespace Tester.Actors
{
    public class PlaybackActor : ReceiveActor
    {
        public PlaybackActor()
        {
            ConsoleLogger.LogMessage("Playback actor created");
            Context.ActorOf(Props.Create<UserCoordinatorActor>(), "UserCoordinator");
            Context.ActorOf(Props.Create<PlayBackStatisticsActor>(), "PlayBackStatistics");


            ///// only when user matches condition message user id == 42
            //Receive<PlayMovieMessage>(message => HandlePlayMovieMessage(message), message => message.UserId == 42);
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new AllForOneStrategy(exception =>
            {
                if (exception is SimulatedCorruptException)
                {
                    return Directive.Restart;
                }
                if (exception is SimulatedTerribleException)
                {
                    return Directive.Resume;
                }

                //fall back to the default behavior
                return Directive.Resume;
            });
        }

        #region lifecycle behavior
        protected override void PreStart()
        {
            ConsoleLogger.LogMessage("PlaybackActor PreStart");

            base.PreStart();
        }
        protected override void PostStop()
        {
            ConsoleLogger.LogMessage("PlaybackActor PostStop");

            base.PostStop();
        }

        protected override void PreRestart(Exception reason, Object message)
        {
            ConsoleLogger.LogMessage("PlaybackActor PpreRestart because " + reason);
            base.PreRestart(reason, message);
        }


        protected override void PostRestart(Exception reason)
        {
            ConsoleLogger.LogMessage("PlaybackActor PostRestart because " + reason);
            base.PostRestart(reason);
        }
        #endregion

    }
}
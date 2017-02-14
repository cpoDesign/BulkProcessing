using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tester.Messages;

namespace Tester.Actors
{
    public class PlaybackActor : ReceiveActor
    {
        public PlaybackActor()
        {
            ConsoleLogger.LogMessage("Playback actor created");
            Receive<PlayMovieMessage>(message => HandlePlayMovieMessage(message));

            /// only when user matches condition message user id == 42
            Receive<PlayMovieMessage>(message => HandlePlayMovieMessage(message), message => message.UserId == 42);
        }
        protected override void PreStart()
        {
            ConsoleLogger.LogMessage($"Playback - PreStart");

            base.PreStart();
        }
        protected override void PostStop()
        {
            ConsoleLogger.LogMessage($"Playback - Post stop");
            base.PostStop();
        }

        private void HandlePlayMovieMessage(PlayMovieMessage message)
        {

            ConsoleLogger.LogMessage($"recieved title:  {message.MovieTitle}");
            ConsoleLogger.LogMessage($"recieved userId: {message.UserId}");

        }
    }

    /*
     * Example for untyped actor 
     
     
      public class PlaybackActor : UntypedActor
    {
        public PlaybackActor()
        {
            ConsoleLogger.LogMessage("Playback actor created");
        }

        protected override void OnReceive(Object message)
        {
            if(message is PlayMovieMessage)
            {
                var m = message as PlayMovieMessage;

                ConsoleLogger.LogMessage($"recieved title: {m.MovieTitle}");
                ConsoleLogger.LogMessage($"recieved userId: {m.UserId}");
            }
            else
            {
                Unhandled(message);
            }
        }
    }*/
}

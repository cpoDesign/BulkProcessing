using System;
using Akka.Actor;
using Tester.Messages;

namespace Tester.Actors
{
    public class UserActor : ReceiveActor
    {
        private string _movieTitle;
        private int _userId;

        public UserActor(int userId)
        {
            _userId = userId;

            Stopped();
            ConsoleLogger.LogMessage("User actor created");
        }

        private void Playing()
        {
            Receive<PlayMovieMessage>(message => ConsoleLogger.ErrorMessage("User is already playing a movie"), message => !string.IsNullOrWhiteSpace(_movieTitle));

            Receive<StopMovieMessage>(message => StopPlayingMove(message));
            ConsoleLogger.LogMessage($"UserActor: {_userId} has become playing");
        }

        private void Stopped()
        {
            // default behavior
            Receive<PlayMovieMessage>(message => StartPlayingMovie(message));
            Receive<StopMovieMessage>(message => ConsoleLogger.ErrorMessage("User is not wathiching a movie and cannot be stopped"), message => string.IsNullOrWhiteSpace(_movieTitle));
            ConsoleLogger.LogMessage($"UserActor: {_userId} has stopped playing");
        }

        private void StartPlayingMovie(PlayMovieMessage message)
        {
            _movieTitle = message.MovieTitle;
            ConsoleLogger.LogMessage($"UserActor: {_userId} has started playing {_movieTitle}");
            Context.ActorSelection("/user/Playback/PlayBackStatistics/MoviePlayCounter").Tell(new IncrementPlayCountMessage(message.MovieTitle));


            Become(Playing);
        }
        private void StopPlayingMove(StopMovieMessage message)
        {
            ConsoleLogger.LogMessage($"UserActor: {_userId} has stopped playing {_movieTitle}");
            _movieTitle = string.Empty;

            Become(Stopped);
        }

        #region lifecycle methods
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
        #endregion
    }
}

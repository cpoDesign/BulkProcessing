using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Tester.Messages;

namespace Tester.Actors
{
    public class UserCoordinatorActor: ReceiveActor
    {
        private readonly Dictionary<int, IActorRef> _users;

        public UserCoordinatorActor()
        {
            _users = new Dictionary<int, IActorRef>();
            Receive<PlayMovieMessage>(message =>
            {
                CreateChildUserIfNotExists(message.UserId);

                IActorRef childActorRef = _users[message.UserId];
                childActorRef.Tell(message);
            });

            Receive<StopMovieMessage>(message =>
            {
                CreateChildUserIfNotExists(message.UserId);

                IActorRef childActorRef = _users[message.UserId];
                childActorRef.Tell(message);
            });
        }

        private void CreateChildUserIfNotExists(int userId)
        {
            if (!_users.ContainsKey(userId))
            {
                IActorRef newChildActorRef = Context.ActorOf(Props.Create(() => new UserActor(userId)), $"User{userId}");
                _users.Add(userId, newChildActorRef);
                ConsoleLogger.LogUserMessage($"UserCoordinatorActor created for userid: {userId} (Total Users: {_users.Count})");
            }
        }

        protected override void PreStart()
        {
            ConsoleLogger.LogUserMessage("UserCoordinatorActor PreStart");

            base.PreStart();
        }
        protected override void PostStop()
        {
            ConsoleLogger.LogUserMessage("UserCoordinatorActor PostStop");

            base.PostStop();
        }

        protected override void PreRestart(Exception reason, Object message)
        {
            ConsoleLogger.LogUserMessage("UserCoordinatorActor PpreRestart because " + reason);
            base.PreRestart(reason, message);
        }


        protected override void PostRestart(Exception reason)
        {
            ConsoleLogger.LogUserMessage("UserCoordinatorActor PostRestart because " + reason);
            base.PostRestart(reason);
        }
    }
}

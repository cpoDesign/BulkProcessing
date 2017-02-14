﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Tester.Actors;
using Tester.Messages;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            const string SystemName = "LaptopDemoSystem";

            ActorSystem BulkProcessingSystem = ActorSystem.Create(SystemName);

            ConsoleLogger.LogMessage("Actor system created");

            Props paybackActorProps = Props.Create<PlaybackActor>();

            IActorRef actorRef = BulkProcessingSystem.ActorOf(paybackActorProps, "PlaybackActor");
            actorRef.Tell(new PlayMovieMessage("Akka.net rocks", 42));
            //Step to kill the instance of the actor
            actorRef.Tell(PoisonPill.Instance);
            // Attempt to send message again
            actorRef.Tell(new PlayMovieMessage("Akka.net rocks", 42));

            Console.ReadKey();
            BulkProcessingSystem.Terminate();



        }
    }

    public static class ConsoleLogger
    {
        public static void LogMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}

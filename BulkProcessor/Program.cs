using Akka.Actor;
using Common;
using System;
using System.Diagnostics;
using BulkProcessor.Actors;
using BulkProcessor.Actors.BatchesProcessor;
using BulkProcessor.Actors.SystemMessages;
using Akka.DI.AutoFac;
using Autofac;
using BulkProcessor.Actors.BatchesProcessor.BulkProcessor.BatchTypeManager.Payments;
using BulkProcessor.DI;

namespace BulkProcessor
{
    class Program
    {
        private static ActorSystem BulkProcessingActorSystem;
        const string SystemName = "BulkProcessingActorSystem";

        static void Main(string[] args)
        {
            ConsoleLogger.LogSystemMessage("Creating BulkProcessingActorSystem");
            BulkProcessingActorSystem = ActorSystem.Create(SystemName);

            // register DI container to the system as : IDependencyResolver
            var container = CreateAutofacContainer();
            new AutoFacDependencyResolver(container, BulkProcessingActorSystem);

            ConsoleLogger.LogSystemMessage("Creating actor supervisory hierarchy");
            // setup the system
            BulkProcessingActorSystem.ActorOf(Props.Create<BulkProcessorActor>(), "BulkProcessorActor");


            var jobTime = Stopwatch.StartNew();

       
            //// send message to start processing the data
            var batchesManager = BulkProcessingActorSystem.ActorOf(Props.Create<BatchesManagerActor>(), "BatchesManagerActor");

            var message = new StartBulkProcessingMessage();

            batchesManager.Tell(message);

            //BulkProcessingActorSystem.Scheduler
            //    .Schedule(TimeSpan.FromSeconds(0),
            //        TimeSpan.FromSeconds(30),
            //        batchesManager,
            //        message);



  

            do
            {
                ShortPause();

                Console.WriteLine();
                ConsoleLogger.LogSystemMessage("enter a command and hit enter");

                var command = Console.ReadLine().ToLowerInvariant();
                ////    if (command.StartsWith("play"))
                ////    {
                ////        //int userId = int.Parse(command.Split(',')[1]);
                ////        //string movieTitle = command.Split(',')[2];

                ////        //var message = new PlayMovieMessage(movieTitle, userId);
                ////        // call actor using user selector using hierarchy
                ////        //BulkProcessingActorSystem.ActorSelection("/user/Playback/UserCoordinator").Tell(message);
                ////    }

                ////    //if (command.startswith("stop"))
                ////    //{
                ////    //    int userid = int.parse(command.split(',')[1]);
                ////    //    var message = new stopmoviemessage(userid);

                ////    //    bulkprocessingactorsystem.actorselection("/user/playback/usercoordinator").tell(message);
                ////    //}

                if (command.StartsWith("exit"))
                {
                    BulkProcessingActorSystem.Terminate();

                    jobTime.Stop();

                    Console.WriteLine("Job complete in {0}ms ", jobTime.ElapsedMilliseconds);
                    ConsoleLogger.LogSystemMessage("Actor system shutdown. Press any key to exit...");
                    Console.ReadLine();

                    Environment.Exit(1);
                }

            } while (true);


            //////// the fact that user actor is created using the props does not mean 
            //////// anything as it needs to be registered with the system to know about it.
            //////Props userActorProps = Props.Create<UserActor>();

            //////IActorRef actorRef = BulkProcessingSystem.ActorOf(paybackActorProps, "PlaybackActor");

            //////actorRef.Tell("Akka.net rocks");
            ////////Step to kill the instance of the actor
            //////actorRef.Tell(PoisonPill.Instance);
            //////// Attempt to send message again => message will be undelivered unless something will pick it up
            //////actorRef.Tell("Akka.net rocks");

            //////Console.ReadKey();
            //////BulkProcessingSystem.Terminate();
        }

        private static IContainer CreateAutofacContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<SystemConfig>().As<ISystemConfig>();
            builder.RegisterType<DemoPaymentGateway>().As<IPaymentGateway>();
            builder.RegisterType<PaymentWorkerActor>();
            builder.RegisterType<JobCoordinatorActor>();
            builder.RegisterType<ConfigActor>();

            var container = builder.Build();
            return container;
        }

        public static void ShortPause()
        {
            System.Threading.Thread.Sleep(1000);
        }
    }
}

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
using BulkProcessor.DataAccess;
using BulkProcessor.DI;

namespace BulkProcessor
{
    class Program
    {
        private static ActorSystem BulkProcessingActorSystem;
        const string SystemName = "BulkProcessingActorSystem";

        static void Main(string[] args)
        {
            BulkProcessingActorSystem = ActorSystem.Create(SystemName);

            // register DI container to the system as : IDependencyResolver
            var container = CreateAutofacContainer();
            new AutoFacDependencyResolver(container, BulkProcessingActorSystem);

            ConsoleLogger.LogSystemMessage("Creating actor supervisory hierarchy");
            
            // setup the system
            BulkProcessingActorSystem.ActorOf(Props.Create<BulkProcessorActor>(), "BulkProcessorActor");
            
            var jobTime = Stopwatch.StartNew();

            // send message to start processing the data
            var batchesManager = BulkProcessingActorSystem.ActorOf(Props.Create<BatchesManagerActor>(), "BatchesManagerActor");

            do
            {
                ShortPause();

                Console.WriteLine();
                ConsoleLogger.LogSystemMessage("enter a command and hit enter");

                var command = Console.ReadLine().ToLowerInvariant().Trim();

                if (command.StartsWith("run"))
                {
                    batchesManager.Tell(new StartBulkProcessingMessage());
                }
                
                if (command.StartsWith("schedule")){
                    BulkProcessingActorSystem.Scheduler
                        .Schedule(TimeSpan.FromSeconds(0),
                                    TimeSpan.FromSeconds(30),
                                    batchesManager,
                                    new StartBulkProcessingMessage());
                }

                if (command.StartsWith("exit"))
                {
                    BulkProcessingActorSystem.Terminate();

                    jobTime.Stop();

                    Console.WriteLine("Job complete in {0}ms ", jobTime.ElapsedMilliseconds);
                    ConsoleLogger.LogSystemMessage("Actor system shutdown. Press any key to exit...");
                    Console.ReadKey();

                    Environment.Exit(1);
                }

            } while (true);
        }

        private static IContainer CreateAutofacContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<SystemConfig>().As<ISystemConfig>();
            builder.RegisterType<DemoPaymentGateway>().As<IPaymentGateway>();
            builder.RegisterType<PersonDataAccess>().As<IPersonDataAccess>();
            builder.RegisterType<PaymentWorkerActor>();
            builder.RegisterType<PaymentJobCoordinatorActor>();
            builder.RegisterType<ConfigActor>();
            builder.RegisterType<PersonCreatorWorker>();
            builder.RegisterType<PeopleJobCoordinatorActor>();

            var container = builder.Build();
            return container;
        }

        public static void ShortPause()
        {
            System.Threading.Thread.Sleep(1000);
        }
    }
}

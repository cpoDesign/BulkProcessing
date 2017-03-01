using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Akka.Actor;
using Akka.DI.Core;
using Akka.Routing;
using BulkProcessor.Actors.BatchesProcessor.BulkProcessor.BatchTypeManager.Payments.Messages;

namespace BulkProcessor.Actors.BatchesProcessor.BulkProcessor.BatchTypeManager.Payments
{
    // Assumes we only get one file per invocation of the console application
    internal class PeopleJobCoordinatorActor : ReceiveActor     
    {
        private readonly IActorRef _validatorWorker;
        private readonly IActorRef _personCreatorWorker;
        private int _numberOfRemainingPeople;

        public PeopleJobCoordinatorActor()
        {
            var props = Props.Create<PersonValidator>().WithRouter(new RoundRobinPool(5));

            _validatorWorker = Context.ActorOf(props, "personValidators");

            _personCreatorWorker = Context.ActorOf(Context.DI().Props<PersonCreatorWorker>().WithRouter(FromConfig.Instance), "personCreatorWorker");

            Receive<ProcessFileMessage>(
                message =>
                {
                    StartNewJob(message.FileName);
                });

            Receive<ProcessValidatedPerson>(
                message =>
                {
                    _numberOfRemainingPeople--;

                    Console.WriteLine($"Person {message.Person.FirstName} {message.Person.LastName} is {(message.IsValid?"valid":"invalid")}");
                    // lets handle the created person
                    if (message.IsValid)
                    {
                        _personCreatorWorker.Tell(new CreatePersonMessage(message.Person.Title, message.Person.FirstName, message.Person.LastName));
                    }
                    else
                    {
                        // TODO: process if failed
                    }


                    var jobIsComplete = _numberOfRemainingPeople == 0;

                    if (jobIsComplete)
                    {
                        //Context.System.Shutdown();
                    }
                });

            Receive<PersonCreated>(message =>
            {
                Console.WriteLine($"Person saved into db");
            });
        }

        private void StartNewJob(string fileName)
        {
            IEnumerable<ValidatePersonRequest> requests = ParseCsvFile(fileName);

            _numberOfRemainingPeople = requests.Count();

            foreach (var sendPaymentMessage in requests)
            {
                _validatorWorker.Tell(sendPaymentMessage);
            }
        }

        private IEnumerable<ValidatePersonRequest> ParseCsvFile(string fileName)
        {
            var fileLines = File.ReadAllLines(fileName);

            foreach (var line in fileLines)
            {
                var values = line.Split(',');

                var message = new ValidatePersonRequest(
                                    values[0],
                                    values[1],
                                    values[2]);

                yield return message;
            }
        }
    }
}
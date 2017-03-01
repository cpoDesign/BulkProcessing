using System;
using Akka.Actor;
using BulkProcessor.Actors.BatchesProcessor.BulkProcessor.BatchTypeManager.Payments.Messages;
using BulkProcessor.DataAccess;
using BulkProcessor.DI;

namespace BulkProcessor.Actors.BatchesProcessor.BulkProcessor.BatchTypeManager.Payments
{
    internal class PersonCreatorWorker : ReceiveActor
    {
        private readonly IPersonDataAccess _personDataAccess;

        public PersonCreatorWorker(IPersonDataAccess personDataAccess)
        {
            if(personDataAccess == null) throw new ArgumentNullException(nameof(personDataAccess));

            _personDataAccess = personDataAccess;

           Receive<CreatePersonMessage>(message => CreatePerson(message));
        }

        private void CreatePerson(CreatePersonMessage message)
        {
            Console.WriteLine("Creating person for {0} {1}", message.FirstName, message.LastName);
            _personDataAccess.InsertPerson(message.Title, message.FirstName, message.LastName);

            Sender.Tell(new PersonCreated());
        }
    }
}
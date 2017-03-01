using System;
using Akka.Actor;
using BulkProcessor.Actors.BatchesProcessor.BulkProcessor.BatchTypeManager.Payments.Messages;
using System.Collections.Generic;

namespace BulkProcessor.Actors.BatchesProcessor.BulkProcessor.BatchTypeManager.Payments
{
    internal class PersonValidator : ReceiveActor
    {
        public PersonValidator()
        {
            Receive<ValidatePersonRequest>(message => SendPayment(message));
        }

        private void SendPayment(ValidatePersonRequest message)
        {
            Console.WriteLine("Validating person {0} {1}", message.FirstName, message.LastName);
            List<string> errors = new List<string>();
            if (string.IsNullOrWhiteSpace(message.FirstName))
            {
                errors.Add("Missing First Name");
            }

            if (string.IsNullOrWhiteSpace(message.LastName))
            {
                errors.Add("Missing Last Name");
            }

            Sender.Tell(new ProcessValidatedPerson(message, string.Join(",", errors)));
        }
    }
}
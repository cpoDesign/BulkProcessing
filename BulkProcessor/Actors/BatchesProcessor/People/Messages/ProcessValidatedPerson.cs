namespace BulkProcessor.Actors.BatchesProcessor.BulkProcessor.BatchTypeManager.Payments.Messages
{
    internal class ProcessValidatedPerson
    {
        public ValidatePersonRequest Person { get; private set; }

        public bool IsValid
        {
            get { return string.IsNullOrWhiteSpace(ValidatorErrors); } 
        }

        public string ValidatorErrors { get; private set; } 

        public ProcessValidatedPerson(ValidatePersonRequest person, string errors = "")
        {
            Person = person;
            this.ValidatorErrors = errors;
        }
    }
}
namespace BulkProcessor.Actors.BatchesProcessor.BulkProcessor.BatchTypeManager.Payments.Messages
{
    internal class CreatePersonMessage
    {
        public string Title { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public CreatePersonMessage(string title, string firstName, string lastName)
        {
            Title = title;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
namespace BulkProcessor.DI
{
    interface IPaymentGateway
    {
        void Pay(int accountNumber, decimal amount);
    }
}
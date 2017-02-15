namespace Tester.Messages
{
    public class StopMovieMessage
    {
        public StopMovieMessage(int userId)
        {
            this.UserId = userId;
        }
        public int UserId { get; private set; }
    }
}

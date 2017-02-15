namespace Tester.Messages
{
    public class IncrementPlayCountMessage
    {
        public string MovieTitle { get; private set; }

        public IncrementPlayCountMessage(string movieTitle)
        {
            this.MovieTitle = movieTitle;
        }
    }
}

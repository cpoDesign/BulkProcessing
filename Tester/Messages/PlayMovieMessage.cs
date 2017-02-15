namespace Tester.Messages
{
    public class PlayMovieMessage
    {
        public PlayMovieMessage(string movieTitle, int userId)
        {
            this.MovieTitle = movieTitle;
            this.UserId = userId;
        }

        public string MovieTitle { get; set; }
        public int UserId { get; set; }
    }
}

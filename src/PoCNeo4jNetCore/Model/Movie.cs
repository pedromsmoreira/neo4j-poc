namespace PoCNeo4jNetCore.Model
{
    public class Movie
    {
        public Movie(string title, string tagLine, int releaseDate)
        {
            this.Title = title;
            this.TagLine = tagLine;
            this.ReleaseDate = releaseDate;
        }

        public string Title { get; set; }

        public string TagLine { get; set; }

        public int ReleaseDate { get; set; }
    }
}
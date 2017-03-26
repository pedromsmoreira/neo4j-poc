namespace Data.Queries.MovieQueries
{
    using System;

    public class GetMoviesByDirectorQuery : BaseQuery
    {
        public GetMoviesByDirectorQuery(string director, int offset) : base(offset)
        {
            this.Director = director;
        }

        private string Director { get; set; }
        
        public override void Validate()
        {
            base.Validate();

            if (string.IsNullOrWhiteSpace(this.Director))
            {
                throw new ArgumentException("Invalid Director Value. Director must not be null or empty.");
            }
        }

        public override void Build()
        {
           this.Query = $"MATCH(director:Person)-[:DIRECTED]->(directorMovies) WHERE director.name = '{this.Director}' RETURN directorMovies.title AS title, directorMovies.released AS released, directorMovies.tagline AS tagline";
        }
    }
}
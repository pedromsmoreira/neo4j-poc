namespace Data.Queries.MovieQueries
{
    using System;

    public class GetAllMoviesQuery : BaseQuery
    {
        public GetAllMoviesQuery(int offSet) : base(offSet)
        {
        }
        
        public override void Build()
        {
            this.Query = $"MATCH (movie:Movie) RETURN movie.title AS title, movie.released AS released, movie.tagline AS tagline LIMIT {this.OffSet}";
        }
    }
}
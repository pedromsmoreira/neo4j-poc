namespace Data.Queries.MovieQueries
{
    using System;

    public class GetMoviesByActorQuery : BaseQuery
    {
        public GetMoviesByActorQuery(string actor, int offset) : base(offset)
        {
            this.Actor = actor;
        }

        public string  Actor { get; set; }

        public override void Validate()
        {
            base.Validate();

            if (string.IsNullOrWhiteSpace(this.Actor))
            {
                throw new ArgumentException("Invalid Actor Value. Actor must not be null or empty.");
            }
        }

        public override void Build()
        {
            this.Query = $"MATCH (actor:Person)-[:ACTED_IN]->(movies) WHERE actor.name = '{this.Actor}' RETURN movies.title AS title, movies.released AS released, movies.tagline AS tagline LIMIT {this.OffSet}";
        }
    }
}
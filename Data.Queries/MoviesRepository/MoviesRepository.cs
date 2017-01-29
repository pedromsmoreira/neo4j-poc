namespace Data.Queries.MoviesRepository
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Domain.Model;
    using Infrastructure.Configuration;
    using Microsoft.Extensions.Options;
    using Neo4j.Driver.V1;

    public class MoviesRepository : IRepository<Movie>
    {
        private readonly Neo4jConfig neo4jConfiguration;

        public MoviesRepository(IOptions<Neo4jConfig> neo4jConfig)
        {
            this.neo4jConfiguration = neo4jConfig.Value;
        }

        IEnumerable<Movie> IRepository<Movie>.Execute(IQuery query)
        {
            var movies = new Collection<Movie>();
            using (var driver = GraphDatabase.Driver(this.neo4jConfiguration.Uri, AuthTokens.Basic(this.neo4jConfiguration.Username, this.neo4jConfiguration.Password)))
            using (var session = driver.Session())
            {
                var result = session.Run(query.Query);

                foreach (var record in result)
                {
                    movies.Add(new Movie(record["title"].As<string>(), record["tagline"].As<string>(), record["released"].As<int>()));
                }
            }

            return movies;
        }
    }
}
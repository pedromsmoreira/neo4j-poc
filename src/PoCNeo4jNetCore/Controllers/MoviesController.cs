// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PoCNeo4jNetCore.Controllers
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Web.Http;
    using Microsoft.AspNetCore.Mvc;
    using Model;
    using Neo4j.Driver.V1;

    [Route("api/[controller]")]
    public partial class MoviesController : Controller
    {
        [HttpGet]
        public IEnumerable<Movie> GetAll([FromUri]int offset = 25)
        {
            var movies = new Collection<Movie>();

            using (var driver = GraphDatabase.Driver("bolt://localhost", AuthTokens.Basic("neo4j", "neo4j")))
            using (var session = driver.Session())
            {
                var result = session.Run($"MATCH (movie:Movie) RETURN movie.title AS title, movie.released AS released, movie.tagline AS tagline LIMIT {offset}");

                foreach (var record in result)
                {
                    movies.Add(new Movie(record["title"].As<string>(), record["tagline"].As<string>(), record["released"].As<int>()));
                }
            }
            return movies;
        }

        [HttpGet]
        [Route("directors")]
        public IEnumerable<Movie> GetMoviesByDirector([FromUri]string director, [FromUri]int offset = 25)
        {
            var movies = new Collection<Movie>();
            var query = $"MATCH(director:Person)-[:DIRECTED]->(directorMovies) WHERE director.name = '{director}' RETURN directorMovies.title AS title, directorMovies.released AS released, directorMovies.tagline AS tagline";

            using (var driver = GraphDatabase.Driver("bolt://localhost", AuthTokens.Basic("neo4j", "neo4j")))
            using (var session = driver.Session())
            {
                var result = session.Run(query);

                foreach (var record in result)
                {
                    movies.Add(new Movie(record["title"].As<string>(), record["tagline"].As<string>(), record["released"].As<int>()));
                }
            }
            return movies;
        }

        [HttpGet]
        [Route("actors")]
        public IEnumerable<Movie> GetMoviesByActor([FromUri]string actor, [FromUri]int offset = 25)
        {
            var movies = new Collection<Movie>();
            var query = $"MATCH (actor:Person)-[:ACTED_IN]->(movies) WHERE actor.name = '{actor}' RETURN movies.title AS title, movies.released AS released, movies.tagline AS tagline LIMIT {offset}";

            using (var driver = GraphDatabase.Driver("bolt://localhost", AuthTokens.Basic("neo4j", "neo4j")))
            using (var session = driver.Session())
            {
                var result = session.Run(query);

                foreach (var record in result)
                {
                    movies.Add(new Movie(record["title"].As<string>(), record["tagline"].As<string>(), record["released"].As<int>()));
                }
            }
            return movies;
        }
    }
}
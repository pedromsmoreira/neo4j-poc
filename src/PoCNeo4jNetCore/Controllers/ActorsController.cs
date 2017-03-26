namespace PoCNeo4jNetCore.Controllers
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Web.Http;

    using Domain.Model;
    using Infrastructure.Configuration;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    using Neo4j.Driver.V1;

    [Route("api/[controller]")]
    public class ActorsController : Controller
    {
        private readonly Neo4jConfig neo4jConfig;

        public ActorsController(IOptions<Neo4jConfig> neo4jConfig)
        {
            this.neo4jConfig = neo4jConfig.Value;
        }

        [HttpGet]
        public IEnumerable<Person> GetActor([FromUri]string actor)
        {
            var actors = new Collection<Person>();

            using (var driver = GraphDatabase.Driver(this.neo4jConfig.Uri, AuthTokens.Basic(this.neo4jConfig.Username, this.neo4jConfig.Password)))
            using (var session = driver.Session())
            {
                var result = session.Run($"MATCH (actor:Person) WHERE actor.name = \"{actor}\" RETURN actor.name AS name, actor.born AS bornIn");

                foreach (var record in result)
                {
                    actors.Add(new Person(record["name"].As<string>(), record["bornIn"].As<int>()));
                }
            }

            return actors;
        }
    }
}
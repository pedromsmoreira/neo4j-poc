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
    public class ActorsController : Controller
    {
        [HttpGet]
        public IEnumerable<Person> GetActor([FromUri]string actor)
        {
            var actors = new Collection<Person>();
            using (var driver = GraphDatabase.Driver("bolt://localhost", AuthTokens.Basic("neo4j", "neo4j")))
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
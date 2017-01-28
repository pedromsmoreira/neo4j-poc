// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PoCNeo4jNetCore.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Neo4j.Driver.V1;

    [Route("api/[controller]")]
    public class MoviesController : Controller
    {
        [HttpGet]
        public IEnumerable<Person> GetAll()
        {
            var actors = new Collection<Person>();
            string actorName = "Hugo Weaving";

            using (var driver = GraphDatabase.Driver("bolt://localhost:7687", AuthTokens.Basic("neo4j", "neo4j")))
            using (var session = driver.Session())
            {
               var result = session.Run($"MATCH (a:Person) WHERE a.name = \"{actorName}\" RETURN a.name AS name");

                foreach (var record in result)
                {
                    actors.Add(new Person(record["name"].As<string>()));
                }
            }
            return actors;
        }

        private List<string> CreateSomeActors()
        {
            List<string> query = new List<string>
            {
                "CREATE (Keanu:Person {name:'Keanu Reeves', born: 1964})",
                "CREATE (Carrie:Person { name: 'Carrie-Anne Moss', born: 1967})",
                "CREATE (Laurence:Person { name: 'Laurence Fishburne', born: 1961})",
                "CREATE (Hugo:Person { name: 'Hugo Weaving', born: 1960})",
                "CREATE (LillyW:Person { name: 'Lilly Wachowski', born: 1967})",
                "CREATE (LanaW:Person { name: 'Lana Wachowski', born: 1965})",
                "CREATE (JoelS:Person { name: 'Joel Silver', born: 1952})"
            };

            return query;

        }

        public class Person
        {
            public Person(string name)
            {
                this.Name = name;
            }

            public string Name { get; set; }
        }
    }
}
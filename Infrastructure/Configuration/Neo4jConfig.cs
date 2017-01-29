namespace Infrastructure.Configuration
{
    public class Neo4jConfig : IDbConfiguration
    {
        public string Uri { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}

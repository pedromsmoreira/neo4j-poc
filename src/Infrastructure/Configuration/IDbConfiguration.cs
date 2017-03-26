namespace Infrastructure.Configuration
{
    public interface IDbConfiguration
    {
        string Uri { get; set; }

        string Username { get; set; }

        string Password { get; set; }
    }
}
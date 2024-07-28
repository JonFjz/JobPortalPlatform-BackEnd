namespace JobPortal.Infrastructure.Configurations
{
    public class Auth0Settings
    {
        public string Domain { get; set; } = null!;
        public string ClientId { get; set; } = null!;
        public string ClientSecret { get; set; } = null!;
        public string Connection { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public string SignupEndpoint { get; set; } = null!;
    }
}

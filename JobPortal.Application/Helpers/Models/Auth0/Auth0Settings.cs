namespace JobPortal.Application.Helpers.Models.Auth0
{
    public class Auth0Settings
    {
        public string Domain { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Connection { get; set; }
        public string Audience { get; set; }
        public string SignupEndpoint { get; set; }
    }
}

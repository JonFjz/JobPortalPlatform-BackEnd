namespace JobPortal.Application.Helpers.Models.Auth0
{
    public class Auth0SignupRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Password { get; set; }


    }
}

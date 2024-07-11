namespace JobPortal.Application.Features.Users.Dtos
{
    public class RegisterDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Password { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;

namespace JobPortal.Domain.Entities.User
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string City { get; set; }
        public string Country { get; set; }

        public ICollection<User_Role> UserRoles { get; set; }
    }
}

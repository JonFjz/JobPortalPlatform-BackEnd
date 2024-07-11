using Microsoft.AspNetCore.Identity;

namespace JobPortal.Domain.Entities.User
{
    public class User_Role  : IdentityUserRole<int>
    {
        public User User { get; set; }
        public Role Role { get; set; }
    }
}

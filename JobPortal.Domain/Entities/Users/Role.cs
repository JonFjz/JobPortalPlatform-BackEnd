using Microsoft.AspNetCore.Identity;

namespace JobPortal.Domain.Entities.User
{
    public class Role : IdentityRole<int>
    {
        public ICollection<User_Role> UserRoles { get; set; }
    }
}

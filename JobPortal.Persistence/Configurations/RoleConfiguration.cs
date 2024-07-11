using JobPortal.Domain.Entities.User;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace JobPortal.Persistence.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
    
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

                
        }
        public static async Task SeedRolesAndAdmin(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            var roles = new List<Role>
            {
                new() {Name = "JobSeeker"},
                new() {Name = "Recruter"},
                new() {Name = "Admin"},
            };


            foreach (var role in roles)
            {
                var roleExists = await roleManager.RoleExistsAsync(role.Name);
                if (!roleExists)
                {
                    await roleManager.CreateAsync(role);
                }
            }

            var adminEmail = "admin@life.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new User
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    FirstName = "",
                    LastName = "",
                    City = "",
                    Country = ""
                };

                var result = await userManager.CreateAsync(adminUser, "LifeAdmin@123"); 

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

        }      
    }
}
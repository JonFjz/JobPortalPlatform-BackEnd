using JobPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobPortal.Persistence.Configurations
{
    public class RatingConfiguration : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {
            builder.HasKey(r => new { r.JobSeekerId, r.EmployerId });

            builder.HasOne(r => r.JobSeeker)
                   .WithMany()
                   .HasForeignKey(r => r.JobSeekerId);

            builder.HasOne(r => r.Employer)
                   .WithMany()
                   .HasForeignKey(r => r.EmployerId);
        }
    }
}

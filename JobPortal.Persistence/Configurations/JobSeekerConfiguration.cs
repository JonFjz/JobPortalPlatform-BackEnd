using JobPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Persistence.Configurations
{
    public class JobSeekerConfiguration : IEntityTypeConfiguration<JobSeeker>
    {
        public void Configure(EntityTypeBuilder<JobSeeker> builder)
        {
            builder
                .HasOne(js => js.Resume)
                .WithOne(r => r.JobSeeker)
                .HasForeignKey<Resume>(r => r.JobSeekerId);

          
        }
    }
}

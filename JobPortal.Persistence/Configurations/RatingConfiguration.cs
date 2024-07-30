using JobPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

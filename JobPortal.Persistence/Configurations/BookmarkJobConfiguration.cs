﻿using JobPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Persistence.BookmarkJobs
{
    public class BookmarkJobConfiguration : IEntityTypeConfiguration<BookmarkJob>
    {
        public void Configure(EntityTypeBuilder<BookmarkJob> builder)
        {
            builder.HasKey(bj => new { bj.JobSeekerId, bj.JobPostingId });

            builder.HasOne(bj => bj.JobSeeker)
                   .WithMany()
                   .HasForeignKey(bj => bj.JobSeekerId);

            builder.HasOne(bj => bj.JobPosting)
                   .WithMany()
                   .HasForeignKey(bj => bj.JobPostingId);
        }
    }
}
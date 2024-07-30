using JobPortal.Application.Features.JobPostings.Dtos;
using JobPortal.Domain.Entities;
using JobPortal.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Application.Features.JobApplications.Dtos
{
    public class JobApplicatinForJobSeekerDto
    {
        public int Id { get; set; }
        public DateTime AppliedOn { get; set; } 
        public string JobApplicationStatus { get; set; }

        public JobPostingDto JobPosting { get; set; }
    }
}

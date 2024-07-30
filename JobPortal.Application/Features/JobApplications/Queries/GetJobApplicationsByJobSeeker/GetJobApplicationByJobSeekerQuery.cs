using JobPortal.Application.Features.JobApplications.Dtos;
using JobPortal.Application.Helpers.Models.Pagination;
using JobPortal.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Application.Features.JobApplications.Queries.GetJobPostingByJobSeeker
{
    public class GetJobApplicationByJobSeekerQuery : IRequest<PagedResult<JobApplicatinForJobSeekerDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public GetJobApplicationByJobSeekerQuery(int pageNumber, int pageSize)
        {            
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}

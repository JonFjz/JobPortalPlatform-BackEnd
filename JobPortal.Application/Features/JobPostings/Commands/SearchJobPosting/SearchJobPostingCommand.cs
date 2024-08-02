using System;
using JobPortal.Application.Features.JobPostings.Dtos;
using JobPortal.Domain.Entities;
using MediatR;

namespace JobPortal.Application.Features.JobPostings.Commands.SearchJobPosting
{
    public class SearchJobPostingCommand : IRequest<List<JobPostingDto>>
    {
        public string SearchTerm { get; }

        public SearchJobPostingCommand(string searchTerm)
        {
            SearchTerm = searchTerm;
        }
    }
}
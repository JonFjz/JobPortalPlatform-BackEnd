using AutoMapper;
using JobPortal.Application.Features.JobPostings.Commands.CreateJobPosting;
using JobPortal.Application.Features.JobPostings.Dtos;
using JobPortal.Domain;

namespace JobPortal.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<JobPosting, JobPostingDto>().ReverseMap();
            CreateMap<CreateJobPostingCommand, JobPosting>();

        }
    }
}

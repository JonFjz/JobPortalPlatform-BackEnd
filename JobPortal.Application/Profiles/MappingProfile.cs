using AutoMapper;
using JobPortal.Application.Features.JobPostings.Commands.CreateJobPosting;
using JobPortal.Application.Features.JobPostings.Dtos;
using JobPortal.Application.Features.Users.Dtos;
using JobPortal.Domain.Entities;
using JobPortal.Domain.Entities.User;

namespace JobPortal.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<JobPosting, JobPostingDto>().ReverseMap();
            CreateMap<CreateJobPostingCommand, JobPosting>();


            CreateMap<RegisterDto, User>();
        }
    }
}

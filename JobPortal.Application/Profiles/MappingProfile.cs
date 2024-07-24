using AutoMapper;
using JobPortal.Application.Features.Educations.Commands.CreateEducation;
using JobPortal.Application.Features.Educations.Commands.UpdateEducation;
using JobPortal.Application.Features.Educations.Dtos;
using JobPortal.Application.Features.Employers.Commands.UpdateEmployerProfile;
using JobPortal.Application.Features.Employers.Dtos;
using JobPortal.Application.Features.JobPostings.Commands.CreateJobPosting;
using JobPortal.Application.Features.JobPostings.Dtos;
using JobPortal.Application.Features.JobSeeker.Commands.UpdateJobSeeker;
using JobPortal.Application.Features.JobSeeker.Dtos;
using JobPortal.Application.Features.Resumes.Dtos;
using JobPortal.Application.Features.WorkExperiences.Commands.CreateWorkExperience;
using JobPortal.Application.Features.WorkExperiences.Commands.UpdateWorkExperience;
using JobPortal.Application.Features.WorkExperiences.Dtos;
using JobPortal.Domain.Entities;

namespace JobPortal.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<JobPosting, JobPostingDto>().ReverseMap();
            CreateMap<CreateJobPostingCommand, JobPosting>();


            CreateMap<JobSeekerRegistrationDto, JobSeeker>();
            CreateMap<EmployerRegistrationDto, Employer>();

            CreateMap<JobSeeker, JobSeekerDto>().ReverseMap();
            CreateMap<JobSeeker, UpdateJobSeekerProfileCommand>().ReverseMap();

            CreateMap<Employer, EmployerDto>().ReverseMap();
            CreateMap<Employer, UpdateEmployerProfileCommand>().ReverseMap();


            CreateMap<JobPosting, JobPostingDto>().ReverseMap();
            CreateMap<CreateJobPostingCommand, JobPosting>();


            CreateMap<Education, EducationDto>().ReverseMap();
            CreateMap<CreateEducationCommand, Education>();
            CreateMap<UpdateEducationCommand, Education>().ReverseMap();


            CreateMap<WorkExperience, WorkExperienceDto>().ReverseMap();
            CreateMap<CreateWorkExperienceCommand, WorkExperience>();
            CreateMap<UpdateWorkExperienceCommand, WorkExperience>().ReverseMap();



            CreateMap<Resume, ResumeDto>().ReverseMap();
        }


    }
}

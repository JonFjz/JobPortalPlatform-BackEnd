using AutoMapper;
using JobPortal.Application.Features.BookmarkJobs.Dtos;
using JobPortal.Application.Features.Educations.Commands.CreateEducation;
using JobPortal.Application.Features.Educations.Commands.UpdateEducation;
using JobPortal.Application.Features.Educations.Dtos;
using JobPortal.Application.Features.Employers.Commands.UpdateEmployerProfile;
using JobPortal.Application.Features.Employers.Dtos;
using JobPortal.Application.Features.JobApplications.Command.ApplyToJob;
using JobPortal.Application.Features.JobApplications.Dtos;
using JobPortal.Application.Features.JobPostings.Commands.CreateJobPosting;
using JobPortal.Application.Features.JobPostings.Commands.SearchJobPosting;
using JobPortal.Application.Features.JobPostings.Commands.UpdateJobPosting;
using JobPortal.Application.Features.JobPostings.Dtos;
using JobPortal.Application.Features.JobSeeker.Commands.UpdateJobSeeker;
using JobPortal.Application.Features.JobSeeker.Dtos;
using JobPortal.Application.Features.Photos.Dtos;
using JobPortal.Application.Features.Resumes.Dtos;
using JobPortal.Application.Features.Reviews.Dtos;
using JobPortal.Application.Features.Skills.Commands.CreateSkill;
using JobPortal.Application.Features.Skills.Commands.UpdateSkill;
using JobPortal.Application.Features.Skills.Dtos;
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
            
            CreateMap<CreateJobPostingCommand, JobPosting>();


            CreateMap<JobSeekerRegistrationDto, JobSeeker>();
            CreateMap<EmployerRegistrationDto, Employer>();

            CreateMap<JobSeeker, JobSeekerDto>().ReverseMap();
            CreateMap<JobSeeker, UpdateJobSeekerProfileCommand>().ReverseMap();

            CreateMap<Employer, EmployerDto>().ReverseMap();
            CreateMap<Employer, UpdateEmployerProfileCommand>().ReverseMap();
            CreateMap<Employer, EmployerOverviewDto>();

            CreateMap<CreateJobPostingCommand, JobPosting>();
            CreateMap<UpdateJobPostingCommand, JobPosting>();
            CreateMap<SearchJobPostingCommand, JobPosting>();
            CreateMap<JobPosting, JobPostingOverviewDto>().ReverseMap();

            CreateMap<JobApplication, JobApplicatinForJobSeekerDto>()
            .ForMember(dest => dest.JobPosting, opt => opt.MapFrom(src => src.JobPosting));

            CreateMap<JobApplication, JobApplicatinForJobSeekerDto>().ReverseMap();

            CreateMap<JobPosting, JobPostingDto>()
                .ForMember(dest => dest.EmployerId, opt => opt.MapFrom(src => src.EmployerId))
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Employer.CompanyName));

            CreateMap<Skill, SkillDto>().ReverseMap();
            CreateMap<CreateSkillCommand, Skill>();
            CreateMap<UpdateSkillCommand, Skill>().ReverseMap();

            CreateMap<Education, EducationDto>().ReverseMap();
            CreateMap<CreateEducationCommand, Education>();
            CreateMap<UpdateEducationCommand, Education>().ReverseMap();


            CreateMap<WorkExperience, WorkExperienceDto>().ReverseMap();
            CreateMap<CreateWorkExperienceCommand, WorkExperience>();
            CreateMap<UpdateWorkExperienceCommand, WorkExperience>().ReverseMap();

          
            CreateMap<Resume, ResumeDto>().ReverseMap();

            CreateMap<Photo, PhotoDto>().ReverseMap();

            CreateMap<ApplyToJobCommand, JobApplication>();
                //.ForMember(dest => dest.ResumeBlobName, opt => opt.Ignore())
                //.ForMember(dest => dest.JobApplicationStatus, opt => opt.MapFrom(src => JobApplicationStatus.Pending));


            CreateMap<JobApplication, JobApplicationDto>().ReverseMap();

            CreateMap<BookmarkJob, BookmarkedJobDto>()
            .ForMember(dest => dest.JobPostingId, opt => opt.MapFrom(src => src.JobPosting.Id))
            .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.JobPosting.Title))
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.JobPosting.Employer.CompanyName));

            CreateMap<Review, ReviewDto>().ReverseMap();
            
            CreateMap<JobPosting, MyPremiumJobPostingDto>().ReverseMap();
            CreateMap<JobPosting, MyJobPostingDto>().ReverseMap();
        }
    }
}

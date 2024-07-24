using AutoMapper;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Application.Features.Employers.Dtos;
using JobPortal.Application.Features.JobSeeker.Dtos;
using JobPortal.Domain.Entities;

namespace JobPortal.Infrastructure.Authentication.Services
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserRegistrationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task SaveJobSeekerAsync(JobSeekerRegistrationDto request, string auth0Id)
        {
            var jobSeeker = _mapper.Map<JobSeeker>(request);
            jobSeeker.Auth0Id = auth0Id;

            await _unitOfWork.Repository<JobSeeker>().CreateAsync(jobSeeker);
            _unitOfWork.Complete();
        }

        public async Task SaveEmployerAsync(EmployerRegistrationDto request, string auth0Id)
        {
            var employer = _mapper.Map<Employer>(request);
            employer.Auth0Id = auth0Id;

            await _unitOfWork.Repository<Employer>().CreateAsync(employer);
            _unitOfWork.Complete();
        }
    }
}
using AutoMapper;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Domain.Entities;
using MediatR;

namespace JobPortal.Application.Features.JobPostings.Commands.CreateJobPosting
{
    public class CreateJobPostingCommandHandler: IRequestHandler<CreateJobPostingCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateJobPostingCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<int> Handle(CreateJobPostingCommand request, CancellationToken cancellationToken)
        {
            var jobPostingToCreate = _mapper.Map<JobPosting>(request);
            await _unitOfWork.Repository<JobPosting>().CreateAsync(jobPostingToCreate);

            return jobPostingToCreate.Id;
        }
    }
}

using AutoMapper;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Application.Features.Educations.Dtos;
using JobPortal.Domain.Entities;
using MediatR;

namespace JobPortal.Application.Features.Educations.Queries.GetEducationById
{
    public class GetEducationByIdQueryHandler : IRequestHandler<GetEducationByIdQuery, EducationDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;
        private readonly IMapper _mapper;

        public GetEducationByIdQueryHandler(IUnitOfWork unitOfWork, IClaimsPrincipalAccessor claimsPrincipalAccessor, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _claimsPrincipalAccessor = claimsPrincipalAccessor;
            _mapper = mapper;
        }
        public async Task<EducationDto> Handle(GetEducationByIdQuery request, CancellationToken cancellationToken)
        {
            var jobSeeker = await _claimsPrincipalAccessor.GetCurrentJobSeekerAsync();

            var education = await _unitOfWork.Repository<Education>().GetByIdAsync(e => e.Id == request.Id);

            if (education == null || education.JobSeekerId != jobSeeker.Id)
            {
                throw new KeyNotFoundException("Education not found.");
            }

            return _mapper.Map<EducationDto>(education);
        }
    }
}

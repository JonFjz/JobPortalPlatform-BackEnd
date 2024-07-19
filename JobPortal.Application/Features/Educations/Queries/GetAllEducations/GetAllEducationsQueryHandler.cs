using AutoMapper;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Application.Features.Educations.Dtos;
using JobPortal.Domain.Entities;
using MediatR;

namespace JobPortal.Application.Features.Educations.Queries.GetAllEducations
{
    public class GetAllEducationsQueryHandler : IRequestHandler<GetAllEducationsQuery, List<EducationDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;
        private readonly IMapper _mapper;

        public GetAllEducationsQueryHandler(IUnitOfWork unitOfWork, IClaimsPrincipalAccessor claimsPrincipalAccessor, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _claimsPrincipalAccessor = claimsPrincipalAccessor;
            _mapper = mapper;
        }

        public async Task<List<EducationDto>> Handle(GetAllEducationsQuery request, CancellationToken cancellationToken)
        {
            var jobSeeker = await _claimsPrincipalAccessor.GetCurrentJobSeekerAsync();

            var educations = await _unitOfWork.Repository<Education>()
                .GetByConditionAsync(e => e.JobSeekerId == jobSeeker.Id);

            return _mapper.Map<List<EducationDto>>(educations);
        }
    }
}

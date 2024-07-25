using AutoMapper;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Application.Features.Employers.Dtos;
using JobPortal.Application.Helpers.Models.Pagination;
using JobPortal.Domain.Entities;
using MediatR;

namespace JobPortal.Application.Features.Employers.Queries.GetEmployersByIndustry
{
    public class GetEmployersByIndustryQueryHandler : IRequestHandler<GetEmployersByIndustryQuery, PagedResult<EmployerOverviewDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetEmployersByIndustryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResult<EmployerOverviewDto>> Handle(GetEmployersByIndustryQuery request, CancellationToken cancellationToken)
        {
            var totalCount = await _unitOfWork.Repository<Employer>()
                .CountByConditionAsync(e => e.Industry == request.Industry);

            var employers = await _unitOfWork.Repository<Employer>()
                .GetPagedByConditionAsync(
                    e => e.Industry == request.Industry,
                    (request.PageNumber - 1) * request.PageSize,
                    request.PageSize);

            var employerDtos = _mapper.Map<List<EmployerOverviewDto>>(employers);

            return new PagedResult<EmployerOverviewDto>
            {
                Items = employerDtos,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}

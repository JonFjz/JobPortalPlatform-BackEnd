using JobPortal.Application.Features.Employers.Dtos;
using JobPortal.Application.Helpers.Models.Pagination;
using JobPortal.Domain.Enums;
using MediatR;

namespace JobPortal.Application.Features.Employers.Queries.GetEmployersByIndustry
{
    public class GetEmployersByIndustryQuery : IRequest<PagedResult<EmployerOverviewDto>>
    {

        public Industry Industry { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetEmployersByIndustryQuery(Industry industry, int pageNumber, int pageSize)
        {
            Industry = industry;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
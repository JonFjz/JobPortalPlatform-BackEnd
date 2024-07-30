using JobPortal.Application.Features.Reviews.Dtos;
using JobPortal.Application.Helpers.Models.Pagination;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Application.Features.Reviews.Queries.GetAllReview
{
    public class GetAllReviewQuery:IRequest<PagedResult<ReviewDto>>
    {
        public int EmployerId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public GetAllReviewQuery(int employerID, int pageNumber, int pageSize)
        {
            EmployerId = employerID;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}

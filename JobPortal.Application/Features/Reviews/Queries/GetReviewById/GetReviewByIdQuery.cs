using JobPortal.Application.Features.Reviews.Dtos;
using MediatR;

namespace JobPortal.Application.Features.Reviews.Queries.GetReviewById
{
    public class GetReviewByIdQuery:IRequest<ReviewDto>
    {
        public int Id { get; set; }
        public GetReviewByIdQuery(int id)
        {
            Id = id;
        }
    }
}

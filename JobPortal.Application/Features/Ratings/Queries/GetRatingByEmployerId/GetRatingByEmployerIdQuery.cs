using MediatR;

namespace JobPortal.Application.Features.Ratings.Queries.GetRatingByEmployerId
{
    public class GetRatingByEmployerIdQuery:IRequest<double>
    {
        public int EmployerId { get; set; }

        public GetRatingByEmployerIdQuery(int employerId)
        {
            EmployerId = employerId;            
        }
    }
}

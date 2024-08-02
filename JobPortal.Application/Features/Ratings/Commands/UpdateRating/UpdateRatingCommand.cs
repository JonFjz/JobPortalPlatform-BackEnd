using MediatR;

namespace JobPortal.Application.Features.Ratings.Commands.UpdateRating
{
    public class UpdateRatingCommand:IRequest
    {
        public int EmployerId { get; set; }
        public int Score { get; set; }
    }
}

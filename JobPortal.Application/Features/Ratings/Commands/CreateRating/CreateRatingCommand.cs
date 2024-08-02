using MediatR;

namespace JobPortal.Application.Features.Ratings.Commands.CreateRating
{
    public class CreateRatingCommand:IRequest
    {
        public int EmployerId { get; set; }
        public int Score { get; set; }
    }
}

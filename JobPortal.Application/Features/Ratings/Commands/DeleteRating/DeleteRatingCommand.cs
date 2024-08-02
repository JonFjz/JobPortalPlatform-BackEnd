using MediatR;

namespace JobPortal.Application.Features.Ratings.Commands.DeleteRating
{
    public class DeleteRatingCommand:IRequest<bool>
    {
        public int EmployerId { get; set; }
        public DeleteRatingCommand(int employerId)
        {
            EmployerId = employerId;            
        }
    }
}

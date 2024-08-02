using MediatR;

namespace JobPortal.Application.Features.Reviews.Commands.DeleteReview
{
    public class DeleteReviewCommand:IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteReviewCommand(int id)
        {
            Id = id;  
        }
    }
}

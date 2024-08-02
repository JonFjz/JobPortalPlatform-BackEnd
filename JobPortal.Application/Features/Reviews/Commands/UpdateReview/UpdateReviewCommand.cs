using MediatR;

namespace JobPortal.Application.Features.Reviews.Commands.UpdateReview
{
    public class UpdateReviewCommand:IRequest
    {
        public int Id { get; set; }
        public string Content { get; set; }
    }
}

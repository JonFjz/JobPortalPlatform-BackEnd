using MediatR;

namespace JobPortal.Application.Features.Reviews.Commands.CreateReview
{
    public class CreateReviewCommand:IRequest<int>
    {
        
        public int EmployerId { get; set; }
        public string Content { get; set; }
    }
}

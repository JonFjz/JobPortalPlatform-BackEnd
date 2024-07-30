using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

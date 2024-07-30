using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Application.Features.Reviews.Commands.UpdateReview
{
    public class UpdateReviewCommand:IRequest
    {
        public int Id { get; set; }
        public string Content { get; set; }
    }
}

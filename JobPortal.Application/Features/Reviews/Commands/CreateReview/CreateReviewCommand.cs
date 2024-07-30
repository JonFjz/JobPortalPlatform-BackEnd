using JobPortal.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Application.Features.Reviews.Commands.CreateReview
{
    public class CreateReviewCommand:IRequest<int>
    {
        
        public int EmployerId { get; set; }
        public string Content { get; set; }
    }
}

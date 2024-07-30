using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

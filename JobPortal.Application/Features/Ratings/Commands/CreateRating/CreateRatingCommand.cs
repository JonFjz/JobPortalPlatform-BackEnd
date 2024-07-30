using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Application.Features.Ratings.Commands.CreateRating
{
    public class CreateRatingCommand:IRequest
    {
        public int EmployerId { get; set; }
        public int Score { get; set; }
    }
}

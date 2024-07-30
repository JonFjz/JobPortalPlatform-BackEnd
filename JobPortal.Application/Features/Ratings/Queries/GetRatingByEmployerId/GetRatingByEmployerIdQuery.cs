using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Application.Features.Ratings.Queries.GetRatingByEmployerId
{
    public class GetRatingByEmployerIdQuery:IRequest<double>
    {
        public int EmployerId { get; set; }

        public GetRatingByEmployerIdQuery(int employerId)
        {
            EmployerId = employerId;            
        }
    }
}

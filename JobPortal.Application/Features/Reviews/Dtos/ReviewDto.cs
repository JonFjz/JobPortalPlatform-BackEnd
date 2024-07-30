using JobPortal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Application.Features.Reviews.Dtos
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string JobSeekerFirstName { get; set; }
        public string JobSeekerLastName { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

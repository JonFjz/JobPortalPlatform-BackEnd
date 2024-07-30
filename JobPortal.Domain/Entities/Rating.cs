using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Domain.Entities
{
    public class Rating
    {
        public int EmployerId { get; set; }
        public Employer Employer { get; set; }
        public int JobSeekerId { get; set; }
        public JobSeeker JobSeeker { get; set; }
        public int Score { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

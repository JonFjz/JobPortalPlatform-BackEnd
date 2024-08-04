using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Application.Features.ScheduleInterview.Dtos
{
    public class InterviewScheduleRequest
    {
        public int JobApplicationId { get; set; }
       
        public string EmployerEmail { get; set; }
        public string JobSeekerEmail { get; set; }
        public DateTime StartInterviewDateTime { get; set; }
        public DateTime EndInterviewDateTime { get; set; }
    }
}

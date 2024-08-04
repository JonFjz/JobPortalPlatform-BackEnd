
using Google.Apis.Calendar.v3.Data;
using JobPortal.Application.Features.ScheduleInterview.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Application.Contracts.Infrastructure
{
    public interface IGoogleCalendarService
    {
        Task<Event> ScheduleGoogleMeetAsync(InterviewScheduleRequest scheduleInterviewRequest,int employerId);
    }
}

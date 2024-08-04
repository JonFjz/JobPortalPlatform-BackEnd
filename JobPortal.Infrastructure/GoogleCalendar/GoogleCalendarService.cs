using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Features.ScheduleInterview.Dtos;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Event = Google.Apis.Calendar.v3.Data.Event;

namespace JobPortal.Infrastructure.GoogleCalendar
{
    public class GoogleCalendarService : IGoogleCalendarService
    {

        public async Task<Event> ScheduleGoogleMeetAsync(InterviewScheduleRequest scheduleInterviewDto,int employerId)
        {

            string[] Scopes = { "https://www.googleapis.com/auth/calendar", "https://www.googleapis.com/auth/calendar.events" };
            string ApplicationName = "JPP";

            string tokenFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "TokenStorage");
            string credPath = Path.Combine(tokenFolderPath, $"{employerId}_token.json");

            UserCredential credantial;
            using (var stream = new FileStream(Path.Combine(Directory.GetCurrentDirectory(), "credentials.json"), FileMode.Open, FileAccess.Read))
            {
                credantial = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.FromStream(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            };

            var services = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer=credantial,
                ApplicationName=ApplicationName,
            });
                var newEvent = new Event
                {
                    Summary = "Interview",
                    Description = $"Interview for Job Application ID: {scheduleInterviewDto.JobApplicationId}",
                    Start = new EventDateTime { DateTimeDateTimeOffset = scheduleInterviewDto.StartInterviewDateTime, TimeZone = "UTC" },
                    End = new EventDateTime { DateTimeDateTimeOffset = scheduleInterviewDto.EndInterviewDateTime, TimeZone = "UTC" },
                    ConferenceData = new ConferenceData
                    {
                        CreateRequest = new CreateConferenceRequest
                        {
                            RequestId = Guid.NewGuid().ToString(),
                            ConferenceSolutionKey = new ConferenceSolutionKey { Type = "hangoutsMeet" }
                        }
                    },
                    Attendees = new List<EventAttendee>
            {
                new EventAttendee { Email = scheduleInterviewDto.EmployerEmail },
                new EventAttendee { Email = scheduleInterviewDto.JobSeekerEmail }
            }
                };

            var request = services.Events.Insert(newEvent, "primary");
            request.ConferenceDataVersion = 1;
            var createdEvent = await request.ExecuteAsync();

            return createdEvent;
        }
    }
}

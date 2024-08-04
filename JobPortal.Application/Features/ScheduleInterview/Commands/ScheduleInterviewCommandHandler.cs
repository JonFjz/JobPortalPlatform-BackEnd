using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Application.Contracts.Persistence;
using JobPortal.Application.Features.ScheduleInterview.Dtos;
using JobPortal.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobPortal.Application.Features.ScheduleInterview.Commands;

public class ScheduleInterviewCommandHandler:IRequestHandler<ScheduleInterviewCommand,bool>
{
    private readonly IGoogleCalendarService _googleCalendarService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IClaimsPrincipalAccessor _claimsPrincipalAccessor;

    public ScheduleInterviewCommandHandler(IGoogleCalendarService googleCalendarService,IUnitOfWork unitOfWork,IClaimsPrincipalAccessor claimsPrincipalAccessor)
    {
        _googleCalendarService = googleCalendarService;
        _unitOfWork = unitOfWork;
        _claimsPrincipalAccessor = claimsPrincipalAccessor;
    }
    
    public async Task<bool> Handle(ScheduleInterviewCommand request, CancellationToken cancellationToken)
    {
        var employer = await _claimsPrincipalAccessor.GetCurrentEmployerAsync();
        var jobApplication = await _unitOfWork.Repository<JobApplication>()
            .GetByCondition(x => x.Id == request.JobApplicationId)
            .Include(x => x.JobPosting)
            .Include(x=>x.JobSeeker)
            .FirstOrDefaultAsync();
        if (jobApplication == null)
        {
            throw new Exception("JobApplication not found!");
        }

        if (employer == null)
        {
            throw new Exception("Employer not found!");
        }
        if (jobApplication.JobPosting.EmployerId != employer.Id)
        {
            throw new Exception("You can not update status of this application");
        }

       

        var scheduleInterviewRequest = new InterviewScheduleRequest
        {
            JobApplicationId = jobApplication.Id,
            JobSeekerEmail=jobApplication.JobSeeker.Email,
            StartInterviewDateTime=request.StartInterviewDateTime,
            EndInterviewDateTime=request.EndInterviewDateTime,
            EmployerEmail=employer.Email
            
        };
        await _googleCalendarService.ScheduleGoogleMeetAsync(scheduleInterviewRequest,employer.Id);

        return true;

    }
}
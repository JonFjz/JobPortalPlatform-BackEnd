using MediatR;

namespace JobPortal.Application.Features.ScheduleInterview.Commands;

public class ScheduleInterviewCommand:IRequest<bool>
{
    public int JobApplicationId { get; set; }
    public DateTime StartInterviewDateTime { get; set; }
    public DateTime EndInterviewDateTime { get; set; }
}
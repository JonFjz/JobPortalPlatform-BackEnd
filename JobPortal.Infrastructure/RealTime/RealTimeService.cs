using JobPortal.Application.Contracts.Infrastructure;
using JobPortal.Domain.Entities;
using Microsoft.AspNetCore.SignalR;
using JobPortal.Application.Features.Reviews.Dtos;

namespace JobPortal.Infrastructure.RealTime
{
    public class RealTimeService:IRealTimeService
    {
        private readonly IHubContext<RealTimeHub> _hubContext;

        public RealTimeService(IHubContext<RealTimeHub> hubContext)
        {
            _hubContext = hubContext;
        }
        public async Task NotifyNewReview(int employerId, ReviewDto review)
        {
            await _hubContext.Clients.Group(employerId.ToString()).SendAsync("ReceiveReview", review);
        }

        public async Task NotifyNewRating(int employerId, Rating rating)
        {
            await _hubContext.Clients.Group(employerId.ToString()).SendAsync("ReceiveRating", rating);
        }
    }
}

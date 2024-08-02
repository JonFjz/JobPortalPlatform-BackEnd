using JobPortal.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace JobPortal.Infrastructure.RealTime
{
    [Authorize]
    public class RealTimeHub:Hub
    {
        public async Task JoinEmployerGroup(int employerId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, employerId.ToString());
        }

        public async Task LeaveEmployerGroup(int employerId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, employerId.ToString());
        }

        public async Task SendReview(int employerId, Review review)
        {
            await Clients.Group(employerId.ToString()).SendAsync("ReceiveReview", review);
        }

        public async Task SendRating(int employerId, Rating rating)
        {
            await Clients.Group(employerId.ToString()).SendAsync("ReceiveRating", rating);
        }
    }
}

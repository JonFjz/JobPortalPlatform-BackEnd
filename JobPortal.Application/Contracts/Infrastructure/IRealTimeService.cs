using JobPortal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobPortal.Application.Features.Reviews.Dtos;

namespace JobPortal.Application.Contracts.Infrastructure
{
    public interface IRealTimeService
    {
        Task NotifyNewReview(int employerId, ReviewDto review);
        Task NotifyNewRating(int employerId, Rating rating);
    }
}

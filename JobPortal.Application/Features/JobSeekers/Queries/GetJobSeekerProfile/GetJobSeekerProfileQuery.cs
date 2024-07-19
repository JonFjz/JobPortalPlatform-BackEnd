﻿using JobPortal.Application.Features.JobSeeker.Dtos;
using MediatR;

namespace JobPortal.Application.Features.JobSeeker.Queries.GetJobSeekerDetail
{
    public class GetJobSeekerProfileQuery : IRequest<JobSeekerDto>{}
}

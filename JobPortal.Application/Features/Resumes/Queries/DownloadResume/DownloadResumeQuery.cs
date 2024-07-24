using JobPortal.Application.Features.Resumes.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Application.Features.Resumes.Queries.DownloadResume
{
    public class DownloadResumeQuery : IRequest<DownloadResumeResult>
    {
    }

}